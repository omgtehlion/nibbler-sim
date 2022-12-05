using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class FlickerFreeListView : ListView
{
    bool isInWmPaintMsg;

    public event MouseEventHandler RealMouseDown;

    public event MouseEventHandler RealMouseUp;

    [StructLayout(LayoutKind.Sequential)]
    public struct NMHDR
    {
        public IntPtr hwndFrom;
        public IntPtr idFrom;
        public int code;
    }

    public FlickerFreeListView() : base() { DoubleBuffered = true; }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg) {
            case 0x0F: // WM_PAINT
                isInWmPaintMsg = true;
                base.WndProc(ref m);
                isInWmPaintMsg = false;
                break;
            case 0x204E: // WM_REFLECT_NOTIFY
                NMHDR nmhdr = (NMHDR)m.GetLParam(typeof(NMHDR));
                if (nmhdr.code == -12) { // NM_CUSTOMDRAW
                    if (isInWmPaintMsg)
                        base.WndProc(ref m);
                } else {
                    base.WndProc(ref m);
                }
                break;
            case 0x0201: // WM_LBUTTONDOWN
                base.WndProc(ref m);
                InvokeLeftMouseEvt(RealMouseDown);
                break;
            case 0x0202: // WM_LBUTTONUP
                base.WndProc(ref m);
                InvokeLeftMouseEvt(RealMouseUp);
                break;
            default:
                base.WndProc(ref m);
                break;
        }
    }

    private void InvokeLeftMouseEvt(MouseEventHandler handler)
    {
        if (handler != null) {
            var pt = PointToClient(Cursor.Position);
            handler(this, new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
        }
    }
}
