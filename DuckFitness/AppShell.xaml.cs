﻿namespace DuckFitness
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Running), typeof(Running));
        }
    }
}