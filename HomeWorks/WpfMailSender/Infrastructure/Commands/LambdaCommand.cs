﻿using System;
using WpfMailSender.Infrastructure.Commands.Base;

namespace WpfMailSender.Infrastructure.Commands
{
    class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        protected override bool CanExecute(object p) => _canExecute?.Invoke(p) ?? true;
        protected override void Execute(object p) => _execute(p);
    }
}
