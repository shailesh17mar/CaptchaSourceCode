using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tavisca.TravelNxt.ControlLoader
{
    [ToolboxData("<{0}:EmbeddedControlLoader runat=server></{0}:EmbeddedControlLoader>")]
    public class EmbeddedControlLoader : WebControl
    {
        public EmbeddedControlLoader(string virtualPathPrefix)
        {
            if (!IsDesignMode)
            {
                VirtualPathPrefix = virtualPathPrefix;
            }
        }

        static bool IsDesignMode
        {
            get
            {
                return HttpContext.Current == null;
            }
        }

        #region Toolbox properties
        private string mAssemblyName = "";

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public string AssemblyName
        {
            get { return mAssemblyName; }
            set { mAssemblyName = value; }
        }

        private string mControlNamespace = "";

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public string ControlNamespace
        {
            get { return mControlNamespace; }
            set { mControlNamespace = value; }
        }

        private string mControlClassName = "";

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public string ControlClassName
        {
            get { return mControlClassName; }
            set { mControlClassName = value; }
        }
        #endregion

        #region Private members

        private string VirtualPathPrefix { get; set; }
        
        private string Path
        {
            get
            {
                return String.Format("/{0}/{1}/{2}.{3}.ascx", VirtualPathPrefix, AssemblyName, ControlNamespace, ControlClassName);
            }
        }

        protected Control ControlInstance { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ControlInstance = Page.LoadControl(Path);
            Controls.Add(ControlInstance);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (IsDesignMode)
            {
                output.Write(Path);
                return;
            }
            base.RenderContents(output);
        }
        #endregion

        #region Helper members to access UserControl properties

        public void SetControlProperty(string propName, bool value)
        {
            ControlInstance.GetType().GetProperty(propName).SetValue(ControlInstance, value, null);
        }

        public void SetControlProperty(string propName, object value)
        {
            ControlInstance.GetType().GetProperty(propName).SetValue(ControlInstance, value, null);
        }

        public object GetControlProperty(string propName)
        {
            return ControlInstance.GetType().GetProperty(propName).GetValue(ControlInstance, null);
        }
        #endregion
    }
}
