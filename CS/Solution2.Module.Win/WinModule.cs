using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using DevExpress.ExpressApp;

namespace Solution2.Module.Win {
    [ToolboxItemFilter( "Xaf.Platform.Win" )]
    public sealed partial class Solution2WindowsFormsModule : ModuleBase {
        public Solution2WindowsFormsModule() {
            InitializeComponent();
        }
    }
}
