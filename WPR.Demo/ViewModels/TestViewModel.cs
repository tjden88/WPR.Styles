﻿using WPR.MVVM.Commands;
using WPR.MVVM.ViewModels;

namespace WPR.Demo.ViewModels
{
    class TestViewModel: ViewModel
    {
        #region Text : string - Текст

        /// <summary>Текст</summary>
        private string _Text;

        /// <summary>Текст</summary>
        public string Text
        {
            get => _Text;
            set => Set(ref _Text, value);
        }

        #endregion

        #region Command TestTypedCommand - Тестовая типизированная команда

        /// <summary>Тестовая типизированная команда</summary>
        private Command<string> _TestTypedCommand;

        /// <summary>Тестовая типизированная команда</summary>
        public Command<string> TestTypedCommand => _TestTypedCommand
            ??= new Command<string>(OnTestTypedCommandExecuted, CanTestTypedCommandExecute);

        /// <summary>Проверка возможности выполнения - Тестовая типизированная команда</summary>
        private bool CanTestTypedCommandExecute(string s) => !string.IsNullOrEmpty(s);

        /// <summary>Логика выполнения - Тестовая типизированная команда</summary>
        private void OnTestTypedCommandExecuted(string s)
        {
            WPRMessageBox.ShowModal(null, s);
        }

        #endregion
    }
}
