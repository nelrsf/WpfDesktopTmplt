using AvalonDock.Layout;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.ViewModels.Layout
{
    public class DocumentManager
    {
        private readonly IMediator? _mediator;
        private readonly IControlAbstractFactory _controlAbstractFactory;
        public ObservableCollection<LayoutDocument> Documents { get; private set; }

        public DocumentManager(IMediator? mediator, IControlAbstractFactory controlAbstractFactory)
        {
            _mediator = mediator;
            _controlAbstractFactory = controlAbstractFactory;
            Documents = new ObservableCollection<LayoutDocument>();
            InitializeViews();
        }

        private void InitializeViews()
        {
            LayoutDocument? home = new LayoutDocument
            {
                Title = "Bienvenido",
                Content = _controlAbstractFactory.CreateControl(typeof(WelcomeControl)),
                CanClose = true,
            };
            home.Closed += DeleteDocument;
            Documents.Add(home);
        }

        private void DeleteDocument(object? sender, EventArgs e)
        {
            if (sender == null) return;
            Documents.Remove((LayoutDocument)sender);
        }

        public void AddView(object content, string viewName)
        {
            LayoutDocument? newDocument = new LayoutDocument
            {
                CanClose = true,
                Title = viewName,
                Content = content
            };
            newDocument.Closed += DeleteDocument;
            Documents.Add(newDocument);
            _mediator?.Notify(new TabMessage(typeof(SelectTabCommand), newDocument));
        }
    }
}
