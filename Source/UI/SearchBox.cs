using System;
using System.Collections.Generic;
using UnityEngine;

namespace trwm.Source.UI
{
    public class SearchBox : MonoBehaviour
    {
        public static void Show(string title, List<string> searchableStrings, Action<string> callback)
        {
            var go = new GameObject
            {
                name = "SearchBox"
            };

            var searchBox = go.AddComponent<SearchBox>();
            searchBox.Initialize(title, searchableStrings, callback);
        }
        
        private string _inputText = "";
        private List<string> _allSearchableStrings = new List<string>();
        private List<string> _currentAutocompleteOptions = new List<string>();
        private Action<string> _onSelectionCallback;
        private int _currentAutocompleteIndex = -1;
        private Vector2 _scrollPosition;

        private Rect _windowRect = new Rect(Screen.width / 2f - 150, Screen.height / 2f - 100, 300, 200);
        private bool _isFocused = false; 
        
        private TextEditor _textEditor;
        private string _title;
        
        private void Initialize(string title, List<string> searchableStrings, Action<string> callback)
        {
            _allSearchableStrings = searchableStrings;
            _onSelectionCallback = callback;
            _title = title;
        }
        
        private void OnGUI()
        {
            _windowRect = GUI.Window(0, _windowRect, DrawSearchWindow, _title); 
        }
        
        private void DrawSearchWindow(int windowID)
        {
            GUILayout.BeginVertical();

            GUI.SetNextControlName("SearchInput");
            string newInputText = GUILayout.TextArea(_inputText);

            if (newInputText != _inputText)
            {
                _inputText = newInputText;
                _currentAutocompleteOptions = GetAutocompleteOptions(_inputText);
                _currentAutocompleteIndex = -1;
            }

            if (Event.current.type == EventType.KeyDown)
            {
                if (Event.current.keyCode == KeyCode.Tab && _currentAutocompleteOptions.Count > 0)
                {
                    Event.current.Use();
                    CycleAutocompleteOptions();
                    MoveCursorToEnd();
                }
            }

            // super dumb way of checking for enter because the check above here just doesn't find it.
            if (_inputText.Contains("\n"))
            {
                _inputText = _inputText.Replace("\n", string.Empty);
                SubmitSearch();
            }

            if (GUI.GetNameOfFocusedControl() != "SearchInput" && !_isFocused)
            {
                GUI.FocusControl("SearchInput");
                _isFocused = true;
            }
            else if (GUI.GetNameOfFocusedControl() != "SearchInput" && _isFocused)
            {
                _isFocused = false;
                SubmitSearch();
            }

            GUILayout.Space(10);

            if (_currentAutocompleteOptions.Count > 0)
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(100));
                for (int i = 0; i < _currentAutocompleteOptions.Count; i++)
                {
                    GUI.SetNextControlName($"AutocompleteOption{i}");
                    if (GUILayout.Button(_currentAutocompleteOptions[i], GetAutocompleteButtonStyle(i)))
                    {
                        _inputText = _currentAutocompleteOptions[i];
                        SubmitSearch();
                    }
                }
                GUILayout.EndScrollView();
            }

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        private List<string> GetAutocompleteOptions(string input)
        {
            // todo: could be implemented with some fuzzy finding...
            
            return _allSearchableStrings.FindAll(s => s.ToLower().Contains(input.ToLower()));
        }

        private void CycleAutocompleteOptions()
        {
            _currentAutocompleteIndex = (_currentAutocompleteIndex + 1) % _currentAutocompleteOptions.Count;
            _inputText = _currentAutocompleteOptions[_currentAutocompleteIndex];
        }

        private void SubmitSearch()
        {
            _onSelectionCallback?.Invoke(_inputText);
            _currentAutocompleteOptions.Clear();
            _currentAutocompleteIndex = -1;
            Destroy(gameObject);
        }
        
        private void MoveCursorToEnd()
        {
            if (_textEditor == null)
            {
                _textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
            }
            _textEditor.MoveTextEnd();
        } 

        private GUIStyle GetAutocompleteButtonStyle(int index)
        {
            var style = new GUIStyle(GUI.skin.button);
            if (index == _currentAutocompleteIndex)
            {
                style.normal.textColor = Color.yellow;
            }
            return style;
        } 
    }
}