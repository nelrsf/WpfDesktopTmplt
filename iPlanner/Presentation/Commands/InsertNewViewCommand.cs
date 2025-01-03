﻿using iPlanner.Presentation.Controls.Sidebar;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using iPlanner.Presentation.ViewModels.Layout;

namespace iPlanner.Presentation.Commands
{
    public class InsertNewViewCommand : CommandInputMessageBase<ViewMessage>, ICommand
    {

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute()) return;

            if (message?.ViewName == null) return;

            if (message.Content == null) return;


            MainWindowViewModel? mainWindowViewModel = (message?.window as MainWindow)?.DataContext as MainWindowViewModel;
            if (mainWindowViewModel != null)
            {
                if (message?.Content == null)
                {
                    throw new InvalidOperationException("No se pudo cargar la vista");
                }
                mainWindowViewModel.AddView(message.Content, message.ViewName);
            }

        }
    }
}
