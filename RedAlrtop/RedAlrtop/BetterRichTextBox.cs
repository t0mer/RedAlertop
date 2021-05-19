using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace RedAlrtop
{
    public class BetterRichTextBox : RichTextBox
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "GetScrollInfo")]
        private static extern bool GetScrollInfo(IntPtr handle, int bar, ref SCROLLINFO info);
        delegate void SetTextCallback(Color color, string text);

        [StructLayout(LayoutKind.Sequential)]
        private struct SCROLLINFO
        {
            public System.UInt32 size;
            public System.UInt32 mask;
            public int min;
            public int max;
            public int page;
            public int position;
            public int trackPosition;
        }

        private SCROLLINFO Scroll;

        private bool ScrolledToBottom()
        {
            Scroll = new SCROLLINFO();
            Scroll.size = Convert.ToUInt32(Marshal.SizeOf(Scroll));
            Scroll.mask = 7;
            if (!GetScrollInfo(Handle, 1, ref Scroll))
                return true;
            return ((Scroll.page == 0) || ((Scroll.page + Scroll.position) >= Scroll.max));

        }

        public void AppendLine(Color color, string text, params object[] args)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AppendText);
                this.Invoke(d, new object[] { color, text + "\n"});
            }
            else
            {

                AppendText(color, text, args);
                AppendText(color, "\n");
            }


        }
        public void AppendText(Color color, string text, params object[] args) => AppendText(color, string.Format(text, args));
        public void AppendText(Color color, string text)
        {
            bool Focus = Focused;
            int SelectionStart = this.SelectionStart;
            int SelectionLength = this.SelectionLength;
            bool AutoScroll = ScrolledToBottom();
            if (!AutoScroll)
            {
                if (Focus)
                    Parent.Focus();
                SendMessage(Handle, 1087, 1, 0);
                // Hide selection, prevents auto-scrolling
            }
            this.SelectionStart = TextLength;
            this.SelectionLength = 0;
            SelectionColor = color;
            AppendText(text);
            SelectionColor = ForeColor;
            this.SelectionStart = SelectionStart;
            this.SelectionLength = SelectionLength;
            if (AutoScroll)
            {
                SendMessage(Handle, 277, 7, 0);
                // Reliably scroll to bottom
            }
            else
            {
                SendMessage(Handle, 1087, 0, 0);
                // Unhide selection
                if (Focus)
                    this.Focus();
            }

        }
    }
}
