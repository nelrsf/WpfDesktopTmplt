﻿using System.Collections.ObjectModel;
using System.Windows.Controls;
using iPlanner.Core.Application.Services;
using iPlanner.Core.Entities.Dashboard;
using iPlanner.Presentation.ViewModels;

namespace iPlanner.Presentation.Controls
{
    public partial class DashboardControl : UserControl
    {
        public DashboardViewModel _dashboardViewModel;
        public DashboardControl()
        {
            InitializeComponent();
            _dashboardViewModel = new DashboardViewModel(new DashboardService());
            DataContext = _dashboardViewModel;
            InitializeSalesItems();
        }

        public void InitializeSalesItems()
        {
            _dashboardViewModel.InitializeGrid();
        }


    }
}