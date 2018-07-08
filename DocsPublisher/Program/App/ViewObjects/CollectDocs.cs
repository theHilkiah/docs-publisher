using DocsPublisher.Program.App.DataObjects;
using DocsPublisher.Program.App.MainObjects;
using DocsPublisher.UIView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DocsPublisher.Program.App.ViewObjects
{
    

    class CollectDocs: AppsCore
    {
        public CollectDocs()
        {            
            this.ContentUI = new DocsCollector();
        }
    }
}
