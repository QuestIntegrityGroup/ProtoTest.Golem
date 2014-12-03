using System.Windows.Automation;
using System;

namespace ProtoTest.Golem.Purple.PurpleElements
{
    public class PurpleButton : PurpleElementBase
    {
        
        public PurpleButton(string name, string pPath) : base(name, pPath)
        {
        }

        public PurpleButton(string name, AutomationElement element) : base(name, element)
        {
            
        }

        public bool IsClickable()
       {
            bool clickable = true;
            try
            {
                PurpleElement.GetClickablePoint();
            }
            catch (Exception)
            {
                clickable = false;
            }
            return clickable;
        }
    }
}
