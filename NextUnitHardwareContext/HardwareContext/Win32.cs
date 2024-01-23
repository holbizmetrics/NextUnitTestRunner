using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace NextUnitHardwareContext.Microsoft.Win32
{
    public enum WindowsMessages
    {
        WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000a,
        WM_SETREDRAW = 0x000b,
        WM_SETTEXT = 0x000c,
        WM_GETTEXT = 0x000d,
        WM_GETTEXTLENGTH = 0x000e,
        WM_PAINT = 0x000f,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUIT = 0x0012,
        WM_QUERYOPEN = 0x0013,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_ENDSESSION = 0x0016,
        WM_SHOWWINDOW = 0x0018,
        WM_CTLCOLOR = 0x0019,
        WM_WININICHANGE = 0x001a,
        WM_DEVMODECHANGE = 0x001b,
        WM_ACTIVATEAPP = 0x001c,
        WM_FONTCHANGE = 0x001d,
        WM_TIMECHANGE = 0x001e,
        WM_CANCELMODE = 0x001f,
        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_CHILDACTIVATE = 0x0022,
        WM_QUEUESYNC = 0x0023,
        WM_GETMINMAXINFO = 0x0024,
        WM_PAINTICON = 0x0026,
        WM_ICONERASEBKGND = 0x0027,
        WM_NEXTDLGCTL = 0x0028,
        WM_SPOOLERSTATUS = 0x002a,
        WM_DRAWITEM = 0x002b,
        WM_MEASUREITEM = 0x002c,
        WM_DELETEITEM = 0x002d,
        WM_VKEYTOITEM = 0x002e,
        WM_CHARTOITEM = 0x002f,
        WM_SETFONT = 0x0030,
        WM_GETFONT = 0x0031,
        WM_SETHOTKEY = 0x0032,
        WM_GETHOTKEY = 0x0033,
        WM_QUERYDRAGICON = 0x0037,
        WM_COMPAREITEM = 0x0039,
        WM_GETOBJECT = 0x003d,
        WM_COMPACTING = 0x0041,
        WM_COMMNOTIFY = 0x0044,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_POWER = 0x0048,
        WM_COPYGLOBALDATA = 0x0049,
        WM_COPYDATA = 0x004a,
        WM_CANCELJOURNAL = 0x004b,
        WM_NOTIFY = 0x004e,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE = 0x0051,
        WM_TCARD = 0x0052,
        WM_HELP = 0x0053,
        WM_USERCHANGED = 0x0054,
        WM_NOTIFYFORMAT = 0x0055,
        WM_CONTEXTMENU = 0x007b,
        WM_STYLECHANGING = 0x007c,
        WM_STYLECHANGED = 0x007d,
        WM_DISPLAYCHANGE = 0x007e,
        WM_GETICON = 0x007f,
        WM_SETICON = 0x0080,
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x0084,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,
        WM_GETDLGCODE = 0x0087,
        WM_SYNCPAINT = 0x0088,
        WM_NCMOUSEMOVE = 0x00a0,
        WM_NCLBUTTONDOWN = 0x00a1,
        WM_NCLBUTTONUP = 0x00a2,
        WM_NCLBUTTONDBLCLK = 0x00a3,
        WM_NCRBUTTONDOWN = 0x00a4,
        WM_NCRBUTTONUP = 0x00a5,
        WM_NCRBUTTONDBLCLK = 0x00a6,
        WM_NCMBUTTONDOWN = 0x00a7,
        WM_NCMBUTTONUP = 0x00a8,
        WM_NCMBUTTONDBLCLK = 0x00a9,
        WM_NCXBUTTONDOWN = 0x00ab,
        WM_NCXBUTTONUP = 0x00ac,
        WM_NCXBUTTONDBLCLK = 0x00ad,
        EM_GETSEL = 0x00b0,
        EM_SETSEL = 0x00b1,
        EM_GETRECT = 0x00b2,
        EM_SETRECT = 0x00b3,
        EM_SETRECTNP = 0x00b4,
        EM_SCROLL = 0x00b5,
        EM_LINESCROLL = 0x00b6,
        EM_SCROLLCARET = 0x00b7,
        EM_GETMODIFY = 0x00b8,
        EM_SETMODIFY = 0x00b9,
        EM_GETLINECOUNT = 0x00ba,
        EM_LINEINDEX = 0x00bb,
        EM_SETHANDLE = 0x00bc,
        EM_GETHANDLE = 0x00bd,
        EM_GETTHUMB = 0x00be,
        EM_LINELENGTH = 0x00c1,
        EM_REPLACESEL = 0x00c2,
        EM_SETFONT = 0x00c3,
        EM_GETLINE = 0x00c4,
        EM_LIMITTEXT = 0x00c5,
        EM_SETLIMITTEXT = 0x00c5,
        EM_CANUNDO = 0x00c6,
        EM_UNDO = 0x00c7,
        EM_FMTLINES = 0x00c8,
        EM_LINEFROMCHAR = 0x00c9,
        EM_SETWORDBREAK = 0x00ca,
        EM_SETTABSTOPS = 0x00cb,
        EM_SETPASSWORDCHAR = 0x00cc,
        EM_EMPTYUNDOBUFFER = 0x00cd,
        EM_GETFIRSTVISIBLELINE = 0x00ce,
        EM_SETREADONLY = 0x00cf,
        EM_SETWORDBREAKPROC = 0x00d0,
        EM_GETWORDBREAKPROC = 0x00d1,
        EM_GETPASSWORDCHAR = 0x00d2,
        EM_SETMARGINS = 0x00d3,
        EM_GETMARGINS = 0x00d4,
        EM_GETLIMITTEXT = 0x00d5,
        EM_POSFROMCHAR = 0x00d6,
        EM_CHARFROMPOS = 0x00d7,
        EM_SETIMESTATUS = 0x00d8,
        EM_GETIMESTATUS = 0x00d9,
        SBM_SETPOS = 0x00e0,
        SBM_GETPOS = 0x00e1,
        SBM_SETRANGE = 0x00e2,
        SBM_GETRANGE = 0x00e3,
        SBM_ENABLE_ARROWS = 0x00e4,
        SBM_SETRANGEREDRAW = 0x00e6,
        SBM_SETSCROLLINFO = 0x00e9,
        SBM_GETSCROLLINFO = 0x00ea,
        SBM_GETSCROLLBARINFO = 0x00eb,
        BM_GETCHECK = 0x00f0,
        BM_SETCHECK = 0x00f1,
        BM_GETSTATE = 0x00f2,
        BM_SETSTATE = 0x00f3,
        BM_SETSTYLE = 0x00f4,
        BM_CLICK = 0x00f5,
        BM_GETIMAGE = 0x00f6,
        BM_SETIMAGE = 0x00f7,
        BM_SETDONTCLICK = 0x00f8,
        WM_INPUT = 0x00ff,
        WM_KEYDOWN = 0x0100,
        WM_KEYFIRST = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_SYSCHAR = 0x0106,
        WM_SYSDEADCHAR = 0x0107,
        //WM_UNICHAR / WM_KEYLAST = 0x0109,
        WM_WNT_CONVERTREQUESTEX = 0x0109,
        WM_CONVERTREQUEST = 0x010a,
        WM_CONVERTRESULT = 0x010b,
        WM_INTERIM = 0x010c,
        WM_IME_STARTCOMPOSITION = 0x010d,
        WM_IME_ENDCOMPOSITION = 0x010e,
        WM_IME_COMPOSITION = 0x010f,
        WM_IME_KEYLAST = 0x010f,
        WM_INITDIALOG = 0x0110,
        WM_COMMAND = 0x0111,
        WM_SYSCOMMAND = 0x0112,
        WM_TIMER = 0x0113,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_SYSTIMER = 0x0118,
        WM_MENUSELECT = 0x011f,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_MENURBUTTONUP = 0x0122,
        WM_MENUDRAG = 0x0123,
        WM_MENUGETOBJECT = 0x0124,
        WM_UNINITMENUPOPUP = 0x0125,
        WM_MENUCOMMAND = 0x0126,
        WM_CHANGEUISTATE = 0x0127,
        WM_UPDATEUISTATE = 0x0128,
        WM_QUERYUISTATE = 0x0129,
        WM_CTLCOLORMSGBOX = 0x0132,
        WM_CTLCOLOREDIT = 0x0133,
        WM_CTLCOLORLISTBOX = 0x0134,
        WM_CTLCOLORBTN = 0x0135,
        WM_CTLCOLORDLG = 0x0136,
        WM_CTLCOLORSCROLLBAR = 0x0137,
        WM_CTLCOLORSTATIC = 0x0138,
        WM_MOUSEFIRST = 0x0200,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_MOUSELAST = 0x0209,
        WM_MOUSEWHEEL = 0x020a,
        WM_XBUTTONDOWN = 0x020b,
        WM_XBUTTONUP = 0x020c,
        WM_XBUTTONDBLCLK = 0x020d,
        WM_MOUSEHWHEEL = 0x020e,
        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,
        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,
        WM_POWERBROADCAST = 0x0218,
        WM_DEVICECHANGE = 0x0219,
        WM_MDICREATE = 0x0220,
        WM_MDIDESTROY = 0x0221,
        WM_MDIACTIVATE = 0x0222,
        WM_MDIRESTORE = 0x0223,
        WM_MDINEXT = 0x0224,
        WM_MDIMAXIMIZE = 0x0225,
        WM_MDITILE = 0x0226,
        WM_MDICASCADE = 0x0227,
        WM_MDIICONARRANGE = 0x0228,
        WM_MDIGETACTIVE = 0x0229,
        WM_MDISETMENU = 0x0230,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_DROPFILES = 0x0233,
        WM_MDIREFRESHMENU = 0x0234,
        WM_IME_REPORT = 0x0280,
        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_IME_CONTROL = 0x0283,
        WM_IME_COMPOSITIONFULL = 0x0284,
        WM_IME_SELECT = 0x0285,
        WM_IME_CHAR = 0x0286,
        WM_IME_REQUEST = 0x0288,
        WM_IMEKEYDOWN = 0x0290,
        WM_IME_KEYDOWN = 0x0290,
        WM_IMEKEYUP = 0x0291,
        WM_IME_KEYUP = 0x0291,
        WM_NCMOUSEHOVER = 0x02a0,
        WM_MOUSEHOVER = 0x02a1,
        WM_NCMOUSELEAVE = 0x02a2,
        WM_MOUSELEAVE = 0x02a3,
        WM_CUT = 0x0300,
        WM_COPY = 0x0301,
        WM_PASTE = 0x0302,
        WM_CLEAR = 0x0303,
        WM_UNDO = 0x0304,
        WM_RENDERFORMAT = 0x0305,
        WM_RENDERALLFORMATS = 0x0306,
        WM_DESTROYCLIPBOARD = 0x0307,
        WM_DRAWCLIPBOARD = 0x0308,
        WM_PAINTCLIPBOARD = 0x0309,
        WM_VSCROLLCLIPBOARD = 0x030a,
        WM_SIZECLIPBOARD = 0x030b,
        WM_ASKCBFORMATNAME = 0x030c,
        WM_CHANGECBCHAIN = 0x030d,
        WM_HSCROLLCLIPBOARD = 0x030e,
        WM_QUERYNEWPALETTE = 0x030f,
        WM_PALETTEISCHANGING = 0x0310,
        WM_PALETTECHANGED = 0x0311,
        WM_HOTKEY = 0x0312,
        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
        WM_APPCOMMAND = 0x0319,
        WM_HANDHELDFIRST = 0x0358,
        WM_HANDHELDLAST = 0x035f,
        WM_AFXFIRST = 0x0360,
        WM_AFXLAST = 0x037f,
        WM_PENWINFIRST = 0x0380,
        WM_RCRESULT = 0x0381,
        WM_HOOKRCRESULT = 0x0382,
        WM_GLOBALRCCHANGE = 0x0383,
        WM_PENMISCINFO = 0x0383,
        WM_SKB = 0x0384,
        WM_HEDITCTL = 0x0385,
        WM_PENCTL = 0x0385,
        WM_PENMISC = 0x0386,
        WM_CTLINIT = 0x0387,
        WM_PENEVENT = 0x0388,
        WM_PENWINLAST = 0x038f,
        DDM_SETFMT = 0x0400,
        DM_GETDEFID = 0x0400,
        NIN_SELECT = 0x0400,
        TBM_GETPOS = 0x0400,
        WM_PSD_PAGESETUPDLG = 0x0400,
        WM_USER = 0x0400,
        CBEM_INSERTITEMA = 0x0401,
        DDM_DRAW = 0x0401,
        DM_SETDEFID = 0x0401,
        HKM_SETHOTKEY = 0x0401,
        PBM_SETRANGE = 0x0401,
        RB_INSERTBANDA = 0x0401,
        SB_SETTEXTA = 0x0401,
        TB_ENABLEBUTTON = 0x0401,
        TBM_GETRANGEMIN = 0x0401,
        TTM_ACTIVATE = 0x0401,
        WM_CHOOSEFONT_GETLOGFONT = 0x0401,
        WM_PSD_FULLPAGERECT = 0x0401,
        CBEM_SETIMAGELIST = 0x0402,
        DDM_CLOSE = 0x0402,
        DM_REPOSITION = 0x0402,
        HKM_GETHOTKEY = 0x0402,
        PBM_SETPOS = 0x0402,
        RB_DELETEBAND = 0x0402,
        SB_GETTEXTA = 0x0402,
        TB_CHECKBUTTON = 0x0402,
        TBM_GETRANGEMAX = 0x0402,
        WM_PSD_MINMARGINRECT = 0x0402,
        CBEM_GETIMAGELIST = 0x0403,
        DDM_BEGIN = 0x0403,
        HKM_SETRULES = 0x0403,
        PBM_DELTAPOS = 0x0403,
        RB_GETBARINFO = 0x0403,
        SB_GETTEXTLENGTHA = 0x0403,
        TBM_GETTIC = 0x0403,
        TB_PRESSBUTTON = 0x0403,
        TTM_SETDELAYTIME = 0x0403,
        WM_PSD_MARGINRECT = 0x0403,
        CBEM_GETITEMA = 0x0404,
        DDM_END = 0x0404,
        PBM_SETSTEP = 0x0404,
        RB_SETBARINFO = 0x0404,
        SB_SETPARTS = 0x0404,
        TB_HIDEBUTTON = 0x0404,
        TBM_SETTIC = 0x0404,
        TTM_ADDTOOLA = 0x0404,
        WM_PSD_GREEKTEXTRECT = 0x0404,
        CBEM_SETITEMA = 0x0405,
        PBM_STEPIT = 0x0405,
        TB_INDETERMINATE = 0x0405,
        TBM_SETPOS = 0x0405,
        TTM_DELTOOLA = 0x0405,
        WM_PSD_ENVSTAMPRECT = 0x0405,
        CBEM_GETCOMBOCONTROL = 0x0406,
        PBM_SETRANGE32 = 0x0406,
        RB_SETBANDINFOA = 0x0406,
        SB_GETPARTS = 0x0406,
        TB_MARKBUTTON = 0x0406,
        TBM_SETRANGE = 0x0406,
        TTM_NEWTOOLRECTA = 0x0406,
        WM_PSD_YAFULLPAGERECT = 0x0406,
        CBEM_GETEDITCONTROL = 0x0407,
        PBM_GETRANGE = 0x0407,
        RB_SETPARENT = 0x0407,
        SB_GETBORDERS = 0x0407,
        TBM_SETRANGEMIN = 0x0407,
        TTM_RELAYEVENT = 0x0407,
        CBEM_SETEXSTYLE = 0x0408,
        PBM_GETPOS = 0x0408,
        RB_HITTEST = 0x0408,
        SB_SETMINHEIGHT = 0x0408,
        TBM_SETRANGEMAX = 0x0408,
        TTM_GETTOOLINFOA = 0x0408,
        CBEM_GETEXSTYLE = 0x0409,
        CBEM_GETEXTENDEDSTYLE = 0x0409,
        PBM_SETBARCOLOR = 0x0409,
        RB_GETRECT = 0x0409,
        SB_SIMPLE = 0x0409,
        TB_ISBUTTONENABLED = 0x0409,
        TBM_CLEARTICS = 0x0409,
        TTM_SETTOOLINFOA = 0x0409,
        CBEM_HASEDITCHANGED = 0x040a,
        RB_INSERTBANDW = 0x040a,
        SB_GETRECT = 0x040a,
        TB_ISBUTTONCHECKED = 0x040a,
        TBM_SETSEL = 0x040a,
        TTM_HITTESTA = 0x040a,
        WIZ_QUERYNUMPAGES = 0x040a,
        CBEM_INSERTITEMW = 0x040b,
        RB_SETBANDINFOW = 0x040b,
        SB_SETTEXTW = 0x040b,
        TB_ISBUTTONPRESSED = 0x040b,
        TBM_SETSELSTART = 0x040b,
        TTM_GETTEXTA = 0x040b,
        WIZ_NEXT = 0x040b,
        CBEM_SETITEMW = 0x040c,
        RB_GETBANDCOUNT = 0x040c,
        SB_GETTEXTLENGTHW = 0x040c,
        TB_ISBUTTONHIDDEN = 0x040c,
        TBM_SETSELEND = 0x040c,
        TTM_UPDATETIPTEXTA = 0x040c,
        WIZ_PREV = 0x040c,
        CBEM_GETITEMW = 0x040d,
        RB_GETROWCOUNT = 0x040d,
        SB_GETTEXTW = 0x040d,
        TB_ISBUTTONINDETERMINATE = 0x040d,
        TTM_GETTOOLCOUNT = 0x040d,
        CBEM_SETEXTENDEDSTYLE = 0x040e,
        RB_GETROWHEIGHT = 0x040e,
        SB_ISSIMPLE = 0x040e,
        TB_ISBUTTONHIGHLIGHTED = 0x040e,
        TBM_GETPTICS = 0x040e,
        TTM_ENUMTOOLSA = 0x040e,
        SB_SETICON = 0x040f,
        TBM_GETTICPOS = 0x040f,
        TTM_GETCURRENTTOOLA = 0x040f,
        RB_IDTOINDEX = 0x0410,
        SB_SETTIPTEXTA = 0x0410,
        TBM_GETNUMTICS = 0x0410,
        TTM_WINDOWFROMPOINT = 0x0410,
        RB_GETTOOLTIPS = 0x0411,
        SB_SETTIPTEXTW = 0x0411,
        TBM_GETSELSTART = 0x0411,
        TB_SETSTATE = 0x0411,
        TTM_TRACKACTIVATE = 0x0411,
        RB_SETTOOLTIPS = 0x0412,
        SB_GETTIPTEXTA = 0x0412,
        TB_GETSTATE = 0x0412,
        TBM_GETSELEND = 0x0412,
        TTM_TRACKPOSITION = 0x0412,
        RB_SETBKCOLOR = 0x0413,
        SB_GETTIPTEXTW = 0x0413,
        TB_ADDBITMAP = 0x0413,
        TBM_CLEARSEL = 0x0413,
        TTM_SETTIPBKCOLOR = 0x0413,
        RB_GETBKCOLOR = 0x0414,
        SB_GETICON = 0x0414,
        TB_ADDBUTTONSA = 0x0414,
        TBM_SETTICFREQ = 0x0414,
        TTM_SETTIPTEXTCOLOR = 0x0414,
        RB_SETTEXTCOLOR = 0x0415,
        TB_INSERTBUTTONA = 0x0415,
        TBM_SETPAGESIZE = 0x0415,
        TTM_GETDELAYTIME = 0x0415,
        RB_GETTEXTCOLOR = 0x0416,
        TB_DELETEBUTTON = 0x0416,
        TBM_GETPAGESIZE = 0x0416,
        TTM_GETTIPBKCOLOR = 0x0416,
        RB_SIZETORECT = 0x0417,
        TB_GETBUTTON = 0x0417,
        TBM_SETLINESIZE = 0x0417,
        TTM_GETTIPTEXTCOLOR = 0x0417,
        RB_BEGINDRAG = 0x0418,
        TB_BUTTONCOUNT = 0x0418,
        TBM_GETLINESIZE = 0x0418,
        TTM_SETMAXTIPWIDTH = 0x0418,
        RB_ENDDRAG = 0x0419,
        TB_COMMANDTOINDEX = 0x0419,
        TBM_GETTHUMBRECT = 0x0419,
        TTM_GETMAXTIPWIDTH = 0x0419,
        RB_DRAGMOVE = 0x041a,
        TBM_GETCHANNELRECT = 0x041a,
        TB_SAVERESTOREA = 0x041a,
        TTM_SETMARGIN = 0x041a,
        RB_GETBARHEIGHT = 0x041b,
        TB_CUSTOMIZE = 0x041b,
        TBM_SETTHUMBLENGTH = 0x041b,
        TTM_GETMARGIN = 0x041b,
        RB_GETBANDINFOW = 0x041c,
        TB_ADDSTRINGA = 0x041c,
        TBM_GETTHUMBLENGTH = 0x041c,
        TTM_POP = 0x041c,
        RB_GETBANDINFOA = 0x041d,
        TB_GETITEMRECT = 0x041d,
        TBM_SETTOOLTIPS = 0x041d,
        TTM_UPDATE = 0x041d,
        RB_MINIMIZEBAND = 0x041e,
        TB_BUTTONSTRUCTSIZE = 0x041e,
        TBM_GETTOOLTIPS = 0x041e,
        TTM_GETBUBBLESIZE = 0x041e,
        RB_MAXIMIZEBAND = 0x041f,
        TBM_SETTIPSIDE = 0x041f,
        TB_SETBUTTONSIZE = 0x041f,
        TTM_ADJUSTRECT = 0x041f,
        TBM_SETBUDDY = 0x0420,
        TB_SETBITMAPSIZE = 0x0420,
        TTM_SETTITLEA = 0x0420,
        MSG_FTS_JUMP_VA = 0x0421,
        TB_AUTOSIZE = 0x0421,
        TBM_GETBUDDY = 0x0421,
        TTM_SETTITLEW = 0x0421,
        RB_GETBANDBORDERS = 0x0422,
        MSG_FTS_JUMP_QWORD = 0x0423,
        RB_SHOWBAND = 0x0423,
        TB_GETTOOLTIPS = 0x0423,
        MSG_REINDEX_REQUEST = 0x0424,
        TB_SETTOOLTIPS = 0x0424,
        MSG_FTS_WHERE_IS_IT = 0x0425,
        RB_SETPALETTE = 0x0425,
        TB_SETPARENT = 0x0425,
        RB_GETPALETTE = 0x0426,
        RB_MOVEBAND = 0x0427,
        TB_SETROWS = 0x0427,
        TB_GETROWS = 0x0428,
        TB_GETBITMAPFLAGS = 0x0429,
        TB_SETCMDID = 0x042a,
        RB_PUSHCHEVRON = 0x042b,
        TB_CHANGEBITMAP = 0x042b,
        TB_GETBITMAP = 0x042c,
        MSG_GET_DEFFONT = 0x042d,
        TB_GETBUTTONTEXTA = 0x042d,
        TB_REPLACEBITMAP = 0x042e,
        TB_SETINDENT = 0x042f,
        TB_SETIMAGELIST = 0x0430,
        TB_GETIMAGELIST = 0x0431,
        TB_LOADIMAGES = 0x0432,
        EM_CANPASTE = 0x0432,
        TTM_ADDTOOLW = 0x0432,
        EM_DISPLAYBAND = 0x0433,
        TB_GETRECT = 0x0433,
        TTM_DELTOOLW = 0x0433,
        EM_EXGETSEL = 0x0434,
        TB_SETHOTIMAGELIST = 0x0434,
        TTM_NEWTOOLRECTW = 0x0434,
        EM_EXLIMITTEXT = 0x0435,
        TB_GETHOTIMAGELIST = 0x0435,
        TTM_GETTOOLINFOW = 0x0435,
        EM_EXLINEFROMCHAR = 0x0436,
        TB_SETDISABLEDIMAGELIST = 0x0436,
        TTM_SETTOOLINFOW = 0x0436,
        EM_EXSETSEL = 0x0437,
        TB_GETDISABLEDIMAGELIST = 0x0437,
        TTM_HITTESTW = 0x0437,
        EM_FINDTEXT = 0x0438,
        TB_SETSTYLE = 0x0438,
        TTM_GETTEXTW = 0x0438,
        EM_FORMATRANGE = 0x0439,
        TB_GETSTYLE = 0x0439,
        TTM_UPDATETIPTEXTW = 0x0439,
        EM_GETCHARFORMAT = 0x043a,
        TB_GETBUTTONSIZE = 0x043a,
        TTM_ENUMTOOLSW = 0x043a,
        EM_GETEVENTMASK = 0x043b,
        TB_SETBUTTONWIDTH = 0x043b,
        TTM_GETCURRENTTOOLW = 0x043b,
        EM_GETOLEINTERFACE = 0x043c,
        TB_SETMAXTEXTROWS = 0x043c,
        EM_GETPARAFORMAT = 0x043d,
        TB_GETTEXTROWS = 0x043d,
        EM_GETSELTEXT = 0x043e,
        TB_GETOBJECT = 0x043e,
        EM_HIDESELECTION = 0x043f,
        TB_GETBUTTONINFOW = 0x043f,
        EM_PASTESPECIAL = 0x0440,
        TB_SETBUTTONINFOW = 0x0440,
        EM_REQUESTRESIZE = 0x0441,
        TB_GETBUTTONINFOA = 0x0441,
        EM_SELECTIONTYPE = 0x0442,
        TB_SETBUTTONINFOA = 0x0442,
        EM_SETBKGNDCOLOR = 0x0443,
        TB_INSERTBUTTONW = 0x0443,
        EM_SETCHARFORMAT = 0x0444,
        TB_ADDBUTTONSW = 0x0444,
        EM_SETEVENTMASK = 0x0445,
        TB_HITTEST = 0x0445,
        EM_SETOLECALLBACK = 0x0446,
        TB_SETDRAWTEXTFLAGS = 0x0446,
        EM_SETPARAFORMAT = 0x0447,
        TB_GETHOTITEM = 0x0447,
        EM_SETTARGETDEVICE = 0x0448,
        TB_SETHOTITEM = 0x0448,
        EM_STREAMIN = 0x0449,
        TB_SETANCHORHIGHLIGHT = 0x0449,
        EM_STREAMOUT = 0x044a,
        TB_GETANCHORHIGHLIGHT = 0x044a,
        EM_GETTEXTRANGE = 0x044b,
        TB_GETBUTTONTEXTW = 0x044b,
        EM_FINDWORDBREAK = 0x044c,
        TB_SAVERESTOREW = 0x044c,
        EM_SETOPTIONS = 0x044d,
        TB_ADDSTRINGW = 0x044d,
        EM_GETOPTIONS = 0x044e,
        TB_MAPACCELERATORA = 0x044e,
        EM_FINDTEXTEX = 0x044f,
        TB_GETINSERTMARK = 0x044f,
        EM_GETWORDBREAKPROCEX = 0x0450,
        TB_SETINSERTMARK = 0x0450,
        EM_SETWORDBREAKPROCEX = 0x0451,
        TB_INSERTMARKHITTEST = 0x0451,
        EM_SETUNDOLIMIT = 0x0452,
        TB_MOVEBUTTON = 0x0452,
        TB_GETMAXSIZE = 0x0453,
        EM_REDO = 0x0454,
        TB_SETEXTENDEDSTYLE = 0x0454,
        EM_CANREDO = 0x0455,
        TB_GETEXTENDEDSTYLE = 0x0455,
        EM_GETUNDONAME = 0x0456,
        TB_GETPADDING = 0x0456,
        EM_GETREDONAME = 0x0457,
        TB_SETPADDING = 0x0457,
        EM_STOPGROUPTYPING = 0x0458,
        TB_SETINSERTMARKCOLOR = 0x0458,
        EM_SETTEXTMODE = 0x0459,
        TB_GETINSERTMARKCOLOR = 0x0459,
        EM_GETTEXTMODE = 0x045a,
        TB_MAPACCELERATORW = 0x045a,
        EM_AUTOURLDETECT = 0x045b,
        TB_GETSTRINGW = 0x045b,
        EM_GETAUTOURLDETECT = 0x045c,
        TB_GETSTRINGA = 0x045c,
        EM_SETPALETTE = 0x045d,
        EM_GETTEXTEX = 0x045e,
        EM_GETTEXTLENGTHEX = 0x045f,
        EM_SHOWSCROLLBAR = 0x0460,
        EM_SETTEXTEX = 0x0461,
        TAPI_REPLY = 0x0463,
        ACM_OPENA = 0x0464,
        BFFM_SETSTATUSTEXTA = 0x0464,
        CDM_FIRST = 0x0464,
        CDM_GETSPEC = 0x0464,
        EM_SETPUNCTUATION = 0x0464,
        IPM_CLEARADDRESS = 0x0464,
        WM_CAP_UNICODE_START = 0x0464,
        ACM_PLAY = 0x0465,
        BFFM_ENABLEOK = 0x0465,
        CDM_GETFILEPATH = 0x0465,
        EM_GETPUNCTUATION = 0x0465,
        IPM_SETADDRESS = 0x0465,
        PSM_SETCURSEL = 0x0465,
        UDM_SETRANGE = 0x0465,
        WM_CHOOSEFONT_SETLOGFONT = 0x0465,
        ACM_STOP = 0x0466,
        BFFM_SETSELECTIONA = 0x0466,
        CDM_GETFOLDERPATH = 0x0466,
        EM_SETWORDWRAPMODE = 0x0466,
        IPM_GETADDRESS = 0x0466,
        PSM_REMOVEPAGE = 0x0466,
        UDM_GETRANGE = 0x0466,
        WM_CAP_SET_CALLBACK_ERRORW = 0x0466,
        WM_CHOOSEFONT_SETFLAGS = 0x0466,
        ACM_OPENW = 0x0467,
        BFFM_SETSELECTIONW = 0x0467,
        CDM_GETFOLDERIDLIST = 0x0467,
        EM_GETWORDWRAPMODE = 0x0467,
        IPM_SETRANGE = 0x0467,
        PSM_ADDPAGE = 0x0467,
        UDM_SETPOS = 0x0467,
        WM_CAP_SET_CALLBACK_STATUSW = 0x0467,
        BFFM_SETSTATUSTEXTW = 0x0468,
        CDM_SETCONTROLTEXT = 0x0468,
        EM_SETIMECOLOR = 0x0468,
        IPM_SETFOCUS = 0x0468,
        PSM_CHANGED = 0x0468,
        UDM_GETPOS = 0x0468,
        CDM_HIDECONTROL = 0x0469,
        EM_GETIMECOLOR = 0x0469,
        IPM_ISBLANK = 0x0469,
        PSM_RESTARTWINDOWS = 0x0469,
        UDM_SETBUDDY = 0x0469,
        CDM_SETDEFEXT = 0x046a,
        EM_SETIMEOPTIONS = 0x046a,
        PSM_REBOOTSYSTEM = 0x046a,
        UDM_GETBUDDY = 0x046a,
        EM_GETIMEOPTIONS = 0x046b,
        PSM_CANCELTOCLOSE = 0x046b,
        UDM_SETACCEL = 0x046b,
        EM_CONVPOSITION = 0x046c,
        PSM_QUERYSIBLINGS = 0x046c,
        UDM_GETACCEL = 0x046c,
        MCIWNDM_GETZOOM = 0x046d,
        PSM_UNCHANGED = 0x046d,
        UDM_SETBASE = 0x046d,
        PSM_APPLY = 0x046e,
        UDM_GETBASE = 0x046e,
        PSM_SETTITLEA = 0x046f,
        UDM_SETRANGE32 = 0x046f,
        PSM_SETWIZBUTTONS = 0x0470,
        UDM_GETRANGE32 = 0x0470,
        WM_CAP_DRIVER_GET_NAMEW = 0x0470,
        PSM_PRESSBUTTON = 0x0471,
        UDM_SETPOS32 = 0x0471,
        WM_CAP_DRIVER_GET_VERSIONW = 0x0471,
        PSM_SETCURSELID = 0x0472,
        UDM_GETPOS32 = 0x0472,
        PSM_SETFINISHTEXTA = 0x0473,
        PSM_GETTABCONTROL = 0x0474,
        PSM_ISDIALOGMESSAGE = 0x0475,
        MCIWNDM_REALIZE = 0x0476,
        PSM_GETCURRENTPAGEHWND = 0x0476,
        MCIWNDM_SETTIMEFORMATA = 0x0477,
        PSM_INSERTPAGE = 0x0477,
        EM_SETLANGOPTIONS = 0x0478,
        MCIWNDM_GETTIMEFORMATA = 0x0478,
        PSM_SETTITLEW = 0x0478,
        WM_CAP_FILE_SET_CAPTURE_FILEW = 0x0478,
        EM_GETLANGOPTIONS = 0x0479,
        MCIWNDM_VALIDATEMEDIA = 0x0479,
        PSM_SETFINISHTEXTW = 0x0479,
        WM_CAP_FILE_GET_CAPTURE_FILEW = 0x0479,
        EM_GETIMECOMPMODE = 0x047a,
        EM_FINDTEXTW = 0x047b,
        MCIWNDM_PLAYTO = 0x047b,
        WM_CAP_FILE_SAVEASW = 0x047b,
        EM_FINDTEXTEXW = 0x047c,
        MCIWNDM_GETFILENAMEA = 0x047c,
        EM_RECONVERSION = 0x047d,
        MCIWNDM_GETDEVICEA = 0x047d,
        PSM_SETHEADERTITLEA = 0x047d,
        WM_CAP_FILE_SAVEDIBW = 0x047d,
        EM_SETIMEMODEBIAS = 0x047e,
        MCIWNDM_GETPALETTE = 0x047e,
        PSM_SETHEADERTITLEW = 0x047e,
        EM_GETIMEMODEBIAS = 0x047f,
        MCIWNDM_SETPALETTE = 0x047f,
        PSM_SETHEADERSUBTITLEA = 0x047f,
        MCIWNDM_GETERRORA = 0x0480,
        PSM_SETHEADERSUBTITLEW = 0x0480,
        PSM_HWNDTOINDEX = 0x0481,
        PSM_INDEXTOHWND = 0x0482,
        MCIWNDM_SETINACTIVETIMER = 0x0483,
        PSM_PAGETOINDEX = 0x0483,
        PSM_INDEXTOPAGE = 0x0484,
        DL_BEGINDRAG = 0x0485,
        MCIWNDM_GETINACTIVETIMER = 0x0485,
        PSM_IDTOINDEX = 0x0485,
        DL_DRAGGING = 0x0486,
        PSM_INDEXTOID = 0x0486,
        DL_DROPPED = 0x0487,
        PSM_GETRESULT = 0x0487,
        DL_CANCELDRAG = 0x0488,
        PSM_RECALCPAGESIZES = 0x0488,
        MCIWNDM_GET_SOURCE = 0x048c,
        MCIWNDM_PUT_SOURCE = 0x048d,
        MCIWNDM_GET_DEST = 0x048e,
        MCIWNDM_PUT_DEST = 0x048f,
        MCIWNDM_CAN_PLAY = 0x0490,
        MCIWNDM_CAN_WINDOW = 0x0491,
        MCIWNDM_CAN_RECORD = 0x0492,
        MCIWNDM_CAN_SAVE = 0x0493,
        MCIWNDM_CAN_EJECT = 0x0494,
        MCIWNDM_CAN_CONFIG = 0x0495,
        IE_GETINK = 0x0496,
        IE_MSGFIRST = 0x0496,
        MCIWNDM_PALETTEKICK = 0x0496,
        IE_SETINK = 0x0497,
        IE_GETPENTIP = 0x0498,
        IE_SETPENTIP = 0x0499,
        IE_GETERASERTIP = 0x049a,
        IE_SETERASERTIP = 0x049b,
        IE_GETBKGND = 0x049c,
        IE_SETBKGND = 0x049d,
        IE_GETGRIDORIGIN = 0x049e,
        IE_SETGRIDORIGIN = 0x049f,
        IE_GETGRIDPEN = 0x04a0,
        IE_SETGRIDPEN = 0x04a1,
        IE_GETGRIDSIZE = 0x04a2,
        IE_SETGRIDSIZE = 0x04a3,
        IE_GETMODE = 0x04a4,
        IE_SETMODE = 0x04a5,
        IE_GETINKRECT = 0x04a6,
        WM_CAP_SET_MCI_DEVICEW = 0x04a6,
        WM_CAP_GET_MCI_DEVICEW = 0x04a7,
        WM_CAP_PAL_OPENW = 0x04b4,
        WM_CAP_PAL_SAVEW = 0x04b5,
        IE_GETAPPDATA = 0x04b8,
        IE_SETAPPDATA = 0x04b9,
        IE_GETDRAWOPTS = 0x04ba,
        IE_SETDRAWOPTS = 0x04bb,
        IE_GETFORMAT = 0x04bc,
        IE_SETFORMAT = 0x04bd,
        IE_GETINKINPUT = 0x04be,
        IE_SETINKINPUT = 0x04bf,
        IE_GETNOTIFY = 0x04c0,
        IE_SETNOTIFY = 0x04c1,
        IE_GETRECOG = 0x04c2,
        IE_SETRECOG = 0x04c3,
        IE_GETSECURITY = 0x04c4,
        IE_SETSECURITY = 0x04c5,
        IE_GETSEL = 0x04c6,
        IE_SETSEL = 0x04c7,
        CDM_LAST = 0x04c8,
        EM_SETBIDIOPTIONS = 0x04c8,
        IE_DOCOMMAND = 0x04c8,
        MCIWNDM_NOTIFYMODE = 0x04c8,
        EM_GETBIDIOPTIONS = 0x04c9,
        IE_GETCOMMAND = 0x04c9,
        EM_SETTYPOGRAPHYOPTIONS = 0x04ca,
        IE_GETCOUNT = 0x04ca,
        EM_GETTYPOGRAPHYOPTIONS = 0x04cb,
        IE_GETGESTURE = 0x04cb,
        MCIWNDM_NOTIFYMEDIA = 0x04cb,
        EM_SETEDITSTYLE = 0x04cc,
        IE_GETMENU = 0x04cc,
        EM_GETEDITSTYLE = 0x04cd,
        IE_GETPAINTDC = 0x04cd,
        MCIWNDM_NOTIFYERROR = 0x04cd,
        IE_GETPDEVENT = 0x04ce,
        IE_GETSELCOUNT = 0x04cf,
        IE_GETSELITEMS = 0x04d0,
        IE_GETSTYLE = 0x04d1,
        MCIWNDM_SETTIMEFORMATW = 0x04db,
        EM_OUTLINE = 0x04dc,
        MCIWNDM_GETTIMEFORMATW = 0x04dc,
        EM_GETSCROLLPOS = 0x04dd,
        EM_SETSCROLLPOS = 0x04de,
        EM_SETFONTSIZE = 0x04df,
        EM_GETZOOM = 0x04e0,
        MCIWNDM_GETFILENAMEW = 0x04e0,
        EM_SETZOOM = 0x04e1,
        MCIWNDM_GETDEVICEW = 0x04e1,
        EM_GETVIEWKIND = 0x04e2,
        EM_SETVIEWKIND = 0x04e3,
        EM_GETPAGE = 0x04e4,
        MCIWNDM_GETERRORW = 0x04e4,
        EM_SETPAGE = 0x04e5,
        EM_GETHYPHENATEINFO = 0x04e6,
        EM_SETHYPHENATEINFO = 0x04e7,
        EM_GETPAGEROTATE = 0x04eb,
        EM_SETPAGEROTATE = 0x04ec,
        EM_GETCTFMODEBIAS = 0x04ed,
        EM_SETCTFMODEBIAS = 0x04ee,
        EM_GETCTFOPENSTATUS = 0x04f0,
        EM_SETCTFOPENSTATUS = 0x04f1,
        EM_GETIMECOMPTEXT = 0x04f2,
        EM_ISIME = 0x04f3,
        EM_GETIMEPROPERTY = 0x04f4,
        EM_GETQUERYRTFOBJ = 0x050d,
        EM_SETQUERYRTFOBJ = 0x050e,
        FM_GETFOCUS = 0x0600,
        FM_GETDRIVEINFOA = 0x0601,
        FM_GETSELCOUNT = 0x0602,
        FM_GETSELCOUNTLFN = 0x0603,
        FM_GETFILESELA = 0x0604,
        FM_GETFILESELLFNA = 0x0605,
        FM_REFRESH_WINDOWS = 0x0606,
        FM_RELOAD_EXTENSIONS = 0x0607,
        FM_GETDRIVEINFOW = 0x0611,
        FM_GETFILESELW = 0x0614,
        FM_GETFILESELLFNW = 0x0615,
        WLX_WM_SAS = 0x0659,
        SM_GETSELCOUNT = 0x07e8,
        UM_GETSELCOUNT = 0x07e8,
        WM_CPL_LAUNCH = 0x07e8,
        SM_GETSERVERSELA = 0x07e9,
        UM_GETUSERSELA = 0x07e9,
        WM_CPL_LAUNCHED = 0x07e9,
        SM_GETSERVERSELW = 0x07ea,
        UM_GETUSERSELW = 0x07ea,
        SM_GETCURFOCUSA = 0x07eb,
        UM_GETGROUPSELA = 0x07eb,
        SM_GETCURFOCUSW = 0x07ec,
        UM_GETGROUPSELW = 0x07ec,
        SM_GETOPTIONS = 0x07ed,
        UM_GETCURFOCUSA = 0x07ed,
        UM_GETCURFOCUSW = 0x07ee,
        UM_GETOPTIONS = 0x07ef,
        UM_GETOPTIONS2 = 0x07f0,
        LVM_FIRST = 0x1000,
        LVM_GETBKCOLOR = 0x1000,
        LVM_SETBKCOLOR = 0x1001,
        LVM_GETIMAGELIST = 0x1002,
        LVM_SETIMAGELIST = 0x1003,
        LVM_GETITEMCOUNT = 0x1004,
        LVM_GETITEMA = 0x1005,
        LVM_SETITEMA = 0x1006,
        LVM_INSERTITEMA = 0x1007,
        LVM_DELETEITEM = 0x1008,
        LVM_DELETEALLITEMS = 0x1009,
        LVM_GETCALLBACKMASK = 0x100a,
        LVM_SETCALLBACKMASK = 0x100b,
        LVM_GETNEXTITEM = 0x100c,
        LVM_FINDITEMA = 0x100d,
        LVM_GETITEMRECT = 0x100e,
        LVM_SETITEMPOSITION = 0x100f,
        LVM_GETITEMPOSITION = 0x1010,
        LVM_GETSTRINGWIDTHA = 0x1011,
        LVM_HITTEST = 0x1012,
        LVM_ENSUREVISIBLE = 0x1013,
        LVM_SCROLL = 0x1014,
        LVM_REDRAWITEMS = 0x1015,
        LVM_ARRANGE = 0x1016,
        LVM_EDITLABELA = 0x1017,
        LVM_GETEDITCONTROL = 0x1018,
        LVM_GETCOLUMNA = 0x1019,
        LVM_SETCOLUMNA = 0x101a,
        LVM_INSERTCOLUMNA = 0x101b,
        LVM_DELETECOLUMN = 0x101c,
        LVM_GETCOLUMNWIDTH = 0x101d,
        LVM_SETCOLUMNWIDTH = 0x101e,
        LVM_GETHEADER = 0x101f,
        LVM_CREATEDRAGIMAGE = 0x1021,
        LVM_GETVIEWRECT = 0x1022,
        LVM_GETTEXTCOLOR = 0x1023,
        LVM_SETTEXTCOLOR = 0x1024,
        LVM_GETTEXTBKCOLOR = 0x1025,
        LVM_SETTEXTBKCOLOR = 0x1026,
        LVM_GETTOPINDEX = 0x1027,
        LVM_GETCOUNTPERPAGE = 0x1028,
        LVM_GETORIGIN = 0x1029,
        LVM_UPDATE = 0x102a,
        LVM_SETITEMSTATE = 0x102b,
        LVM_GETITEMSTATE = 0x102c,
        LVM_GETITEMTEXTA = 0x102d,
        LVM_SETITEMTEXTA = 0x102e,
        LVM_SETITEMCOUNT = 0x102f,
        LVM_SORTITEMS = 0x1030,
        LVM_SETITEMPOSITION32 = 0x1031,
        LVM_GETSELECTEDCOUNT = 0x1032,
        LVM_GETITEMSPACING = 0x1033,
        LVM_GETISEARCHSTRINGA = 0x1034,
        LVM_SETICONSPACING = 0x1035,
        LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1036,
        LVM_GETEXTENDEDLISTVIEWSTYLE = 0x1037,
        LVM_GETSUBITEMRECT = 0x1038,
        LVM_SUBITEMHITTEST = 0x1039,
        LVM_SETCOLUMNORDERARRAY = 0x103a,
        LVM_GETCOLUMNORDERARRAY = 0x103b,
        LVM_SETHOTITEM = 0x103c,
        LVM_GETHOTITEM = 0x103d,
        LVM_SETHOTCURSOR = 0x103e,
        LVM_GETHOTCURSOR = 0x103f,
        LVM_APPROXIMATEVIEWRECT = 0x1040,
        LVM_SETWORKAREAS = 0x1041,
        LVM_GETSELECTIONMARK = 0x1042,
        LVM_SETSELECTIONMARK = 0x1043,
        LVM_SETBKIMAGEA = 0x1044,
        LVM_GETBKIMAGEA = 0x1045,
        LVM_GETWORKAREAS = 0x1046,
        LVM_SETHOVERTIME = 0x1047,
        LVM_GETHOVERTIME = 0x1048,
        LVM_GETNUMBEROFWORKAREAS = 0x1049,
        LVM_SETTOOLTIPS = 0x104a,
        LVM_GETITEMW = 0x104b,
        LVM_SETITEMW = 0x104c,
        LVM_INSERTITEMW = 0x104d,
        LVM_GETTOOLTIPS = 0x104e,
        LVM_FINDITEMW = 0x1053,
        LVM_GETSTRINGWIDTHW = 0x1057,
        LVM_GETCOLUMNW = 0x105f,
        LVM_SETCOLUMNW = 0x1060,
        LVM_INSERTCOLUMNW = 0x1061,
        LVM_GETITEMTEXTW = 0x1073,
        LVM_SETITEMTEXTW = 0x1074,
        LVM_GETISEARCHSTRINGW = 0x1075,
        LVM_EDITLABELW = 0x1076,
        LVM_GETBKIMAGEW = 0x108b,
        LVM_SETSELECTEDCOLUMN = 0x108c,
        LVM_SETTILEWIDTH = 0x108d,
        LVM_SETVIEW = 0x108e,
        LVM_GETVIEW = 0x108f,
        LVM_INSERTGROUP = 0x1091,
        LVM_SETGROUPINFO = 0x1093,
        LVM_GETGROUPINFO = 0x1095,
        LVM_REMOVEGROUP = 0x1096,
        LVM_MOVEGROUP = 0x1097,
        LVM_MOVEITEMTOGROUP = 0x109a,
        LVM_SETGROUPMETRICS = 0x109b,
        LVM_GETGROUPMETRICS = 0x109c,
        LVM_ENABLEGROUPVIEW = 0x109d,
        LVM_SORTGROUPS = 0x109e,
        LVM_INSERTGROUPSORTED = 0x109f,
        LVM_REMOVEALLGROUPS = 0x10a0,
        LVM_HASGROUP = 0x10a1,
        LVM_SETTILEVIEWINFO = 0x10a2,
        LVM_GETTILEVIEWINFO = 0x10a3,
        LVM_SETTILEINFO = 0x10a4,
        LVM_GETTILEINFO = 0x10a5,
        LVM_SETINSERTMARK = 0x10a6,
        LVM_GETINSERTMARK = 0x10a7,
        LVM_INSERTMARKHITTEST = 0x10a8,
        LVM_GETINSERTMARKRECT = 0x10a9,
        LVM_SETINSERTMARKCOLOR = 0x10aa,
        LVM_GETINSERTMARKCOLOR = 0x10ab,
        LVM_SETINFOTIP = 0x10ad,
        LVM_GETSELECTEDCOLUMN = 0x10ae,
        LVM_ISGROUPVIEWENABLED = 0x10af,
        LVM_GETOUTLINECOLOR = 0x10b0,
        LVM_SETOUTLINECOLOR = 0x10b1,
        LVM_CANCELEDITLABEL = 0x10b3,
        LVM_MAPINDEXTOID = 0x10b4,
        LVM_MAPIDTOINDEX = 0x10b5,
        LVM_ISITEMVISIBLE = 0x10b6,
        OCM__BASE = 0x2000,
        LVM_SETUNICODEFORMAT = 0x2005,
        LVM_GETUNICODEFORMAT = 0x2006,
        OCM_CTLCOLOR = 0x2019,
        OCM_DRAWITEM = 0x202b,
        OCM_MEASUREITEM = 0x202c,
        OCM_DELETEITEM = 0x202d,
        OCM_VKEYTOITEM = 0x202e,
        OCM_CHARTOITEM = 0x202f,
        OCM_COMPAREITEM = 0x2039,
        OCM_NOTIFY = 0x204e,
        OCM_COMMAND = 0x2111,
        OCM_HSCROLL = 0x2114,
        OCM_VSCROLL = 0x2115,
        OCM_CTLCOLORMSGBOX = 0x2132,
        OCM_CTLCOLOREDIT = 0x2133,
        OCM_CTLCOLORLISTBOX = 0x2134,
        OCM_CTLCOLORBTN = 0x2135,
        OCM_CTLCOLORDLG = 0x2136,
        OCM_CTLCOLORSCROLLBAR = 0x2137,
        OCM_CTLCOLORSTATIC = 0x2138,
        OCM_PARENTNOTIFY = 0x2210,
        WM_APP = 0x8000,
        WM_RASDIALEVENT = 0xcccd,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MENUITEMINFO
    {
        public uint cbSize;
        public uint fMask;
        public uint fType;
        public uint fState;
        public int wID;
        public int  /*HMENU*/      hSubMenu;
        public int  /*HBITMAP*/   hbmpChecked;
        public int  /*HBITMAP*/      hbmpUnchecked;
        public int  /*ULONG_PTR*/ dwItemData;
        public String dwTypeData;
        public uint cch;
        public int /*HBITMAP*/ hbmpItem;
    }

    /// <summary>
    /// For use with ChildWindowFromPointEx 
    /// </summary>
    [Flags]
    public enum WindowFromPointFlags
    {
        /// <summary>
        /// Does not skip any child windows
        /// </summary>
        CWP_ALL = 0x0000,
        /// <summary>
        /// Skips invisible child windows
        /// </summary>
        CWP_SKIPINVISIBLE = 0x0001,
        /// <summary>
        /// Skips disabled child windows
        /// </summary>
        CWP_SKIPDISABLED = 0x0002,
        /// <summary>
        /// Skips transparent child windows
        /// </summary>
        CWP_SKIPTRANSPARENT = 0x0004
    }

    public enum ComboBoxMessages
    {

        CB_GETEDITSEL = 0x0140,
        CB_LIMITTEXT = 0x0141,
        CB_SETEDITSEL = 0x0142,
        CB_ADDSTRING = 0x0143,
        CB_DELETESTRING = 0x0144,
        CB_DIR = 0x0145,
        CB_GETCOUNT = 0x0146,
        CB_GETCURSEL = 0x0147,
        CB_GETLBTEXT = 0x0148,
        CB_GETLBTEXTLEN = 0x0149,
        CB_INSERTSTRING = 0x014A,
        CB_RESETCONTENT = 0x014B,
        CB_FINDSTRING = 0x014C,
        CB_SELECTSTRING = 0x014D,
        CB_SETCURSEL = 0x014E,
        CB_SHOWDROPDOWN = 0x014F,
        CB_GETITEMDATA = 0x0150,
        CB_SETITEMDATA = 0x0151,
        CB_GETDROPPEDCONTROLRECT = 0x0152,
        CB_SETITEMHEIGHT = 0x0153,
        CB_GETITEMHEIGHT = 0x0154,
        CB_SETEXTENDEDUI = 0x0155,
        CB_GETEXTENDEDUI = 0x0156,
        CB_GETDROPPEDSTATE = 0x0157,
        CB_FINDSTRINGEXACT = 0x0158,
        CB_SETLOCALE = 0x0159,
        CB_GETLOCALE = 0x015A,
        CB_GETTOPINDEX = 0x015B,
        CB_SETTOPINDEX = 0x015C,
        CB_GETHORIZONTALEXTENT = 0x015D,
        CB_SETHORIZONTALEXTENT = 0x015E,
        CB_GETDROPPEDWIDTH = 0x015F,
        CB_SETDROPPEDWIDTH = 0x0160,
        CB_INITSTORAGE = 0x0161
        /*  CB_MSGMAX    = 354*/
    }

    public class Win32
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct DebugEvent32
        {
            [FieldOffset(0)]
            public DebugEventHeader header;
            [FieldOffset(12)]
            public DebugEventUnion union;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DebugEvent64
        {
            [FieldOffset(0)]
            public DebugEventHeader header;
            [FieldOffset(16)]
            public DebugEventUnion union;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DebugEventHeader
        {
            public DebugEventCodes dwDebugEventCode;
            public UInt32 dwProcessId;
            public UInt32 dwThreadId;
        }

        public enum DebugEventCodes
        {
            None = 0,
            EXCEPTION_DEBUG_EVENT = 1,
            CREATE_THREAD_DEBUG_EVENT = 2,
            CREATE_PROCESS_DEBUG_EVENT = 3,
            EXIT_THREAD_DEBUG_EVENT = 4,
            EXIT_PROCESS_DEBUG_EVENT = 5,
            LOAD_DLL_DEBUG_EVENT = 6,
            UNLOAD_DLL_DEBUG_EVENT = 7,
            OUTPUT_DEBUG_STRING_EVENT = 8,
            RIP_EVENT = 9,
        }

        public enum ListModules : uint
        {
            LIST_MODULES_DEFAULT = 0x0,
            LIST_MODULES_32BIT = 0x01,
            LIST_MODULES_64BIT = 0x02,
            LIST_MODULES_ALL = 0x03
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BasicProcessInformation
        {
            public IntPtr Reserved1;
            public IntPtr PebBaseAddress;
            public IntPtr Reserved2;
            public IntPtr Reserved2_1;
            public IntPtr UniqueProcessId;
            public IntPtr InheritedFromUniqueProcessId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STARTUP_INFO
        {
            public int cb;
            public string Reserved;
            public string Desktop;
            public string Title;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int Flags;
            public short ShowWindow;
            public short cb2;
            public IntPtr Reserved2;
            public IntPtr StdInput;
            public IntPtr StdOutput;
            public IntPtr StdError;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DebugEventUnion
        {
            [FieldOffset(0)]
            public CREATE_PROCESS_DEBUG_INFO CreateProcess;
            [FieldOffset(0)]
            public EXCEPTION_DEBUG_INFO Exception;
            [FieldOffset(0)]
            public CREATE_THREAD_DEBUG_INFO CreateThread;
            [FieldOffset(0)]
            public EXIT_THREAD_DEBUG_INFO ExitThread;
            [FieldOffset(0)]
            public EXIT_PROCESS_DEBUG_INFO ExitProcess;
            [FieldOffset(0)]
            public LOAD_DLL_DEBUG_INFO LoadDll;
            [FieldOffset(0)]
            public UNLOAD_DLL_DEBUG_INFO UnloadDll;
            [FieldOffset(0)]
            public OUTPUT_DEBUG_STRING_INFO OutputDebugString;
        }

        /// <summary>
		/// Holds the error information for the RIP debug event, only used by Union and should not be created individually
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
        private struct RIP_INFO
        {
            public uint Error;
            public uint Type;
        }

        /// <summary>
		/// Holds the exit code for the thread, only used by Union and should not be created individually
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct EXIT_THREAD_DEBUG_INFO
        {
            public uint ExitCode;
        }

        /// <summary>
		/// Holds process creation details, only used by Union and should not be created individually
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct CREATE_PROCESS_DEBUG_INFO
        {
            public IntPtr File;
            public IntPtr Process;
            public IntPtr Thread;
            public IntPtr BaseOfImage;
            public uint DebugInfoFileOffset;
            public uint DebugInfoSize;
            public IntPtr ThreadLocalBase;
            public IntPtr StartAddress;
            public IntPtr ImageName;
            public ushort Unicode;
        }

        /// <summary>
		/// Holds thread creation details, only used by Union and should not be created individually
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct CREATE_THREAD_DEBUG_INFO
        {
            public IntPtr Thread;
            public IntPtr ThreadLocalBase;
            public IntPtr StartAddress;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DebugEvent
        {
            [FieldOffset(0)]
            public DebugEventHeader header;
            [FieldOffset(12)]
            public DebugEventUnion union;
        }

        [DllImport("advapi32.dll")]
        public static extern bool AbortSystemShutdown(String lpMachineName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcessWorkingSet(IntPtr hProcess, out IntPtr lpMinimumWorkingSetSize, out IntPtr lpMaximumWorkingSetSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FindFileHandle(IntPtr hProcess, IntPtr hFile, out IntPtr lpFileInformation);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern uint EnumProcessModules(IntPtr hProcess, IntPtr[] lphModule, uint cb, out uint lpcbNeeded);

        // see: https://docs.microsoft.com/en-us/windows/win32/api/psapi/nf-psapi-enumprocessmodulesex
        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool EnumProcessModulesEx(
             IntPtr hProcess,
             [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)][In][Out] IntPtr[] lphModule,
             uint cb,
             [MarshalAs(UnmanagedType.U4)] out uint lpcbNeeded,
             ListModules dwFilterFlag
        );

        [DllImport("psapi.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int EnumProcessModules(IntPtr hProcess, [Out] IntPtr lphModule, uint cb, out uint lpcbNeeded);

        [DllImport("kernel32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WaitForDebugEvent(ref DebugEvent pDebugEvent, int dwMilliseconds);

        [DllImport("kernel32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ContinueDebugEvent(uint dwProcessId, uint dwThreadId, ContinueStatus dwContinueStatus);


        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr OpenThread([In] ThreadAccessRights DesiredAccess, [In] bool InheritHandle, [In] uint ThreadID);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool TerminateThread(IntPtr Thread, uint ExitCode);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool TerminateProcess([In] IntPtr Process, [In] uint ExitCode);

        [DllImport("Kernel32.dll")]
        public static extern IntPtr AddVectoredExceptionHandler(uint First, IntPtr Handler);
        //Debuginfromation
        [DllImport("kernel32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DebugActiveProcess(uint dwProcessId);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool DebugBreakProcess([In] IntPtr ProcessHandle);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool DebugActiveProcessStop([In] uint ProcessId);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetThreadContext([In] IntPtr Thread, [In] ref CONTEXT Context);

        //[DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern bool GetThreadContext([In] IntPtr Thread, [In, Out] ref CONTEXT Context);

        //[DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern bool WriteProcessMemory([In] IntPtr Process, [In] IntPtr BaseAddress, [In] byte[] Buffer, uint Size, [Out] out UIntPtr NumberBytesWritten);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetShortPathName([In] string LongPath, [Out] StringBuilder ShortPath, [In] uint BufferLength);

        [DllImport("kernel32")]
        public static extern bool CreateProcess(string lpApplicationName,
                                        string lpCommandLine,
                                        IntPtr lpProcessAttributes,
                                        IntPtr lpThreadAttributes,
                                        bool bInheritHandles,
                                        ProcessCreationFlags dwCreationFlags,
                                        IntPtr lpEnvironment,
                                        string lpCurrentDirectory,
                                        ref STARTUPINFO lpStartupInfo,
                                        out PROCESS_INFORMATION lpProcessInformation);

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        public enum ThreadAccessRights : uint
        {
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            SYNCHRONIZE = 0x00100000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            THREAD_GET_CONTEXT = 0x0008
        }

        public enum ProcessCreationFlags : uint
        {
            CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
            CREATE_DEFAULT_ERROR_MODE = 0x04000000,
            CREATE_NEW_CONSOLE = 0x00000010,
            CREATE_NEW_PROCESS_GROUP = 0x00000200,
            CREATE_NO_WINDOW = 0x08000000,
            CREATE_PROTECTED_PROCESS = 0x00040000,
            CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
            CREATE_SEPARATE_WOW_VDM = 0x00000800,
            CREATE_SHARED_WOW_VDM = 0x00001000,
            CREATE_SUSPENDED = 0x00000004,
            CREATE_UNICODE_ENVIRONMENT = 0x00000400,
            DEBUG_ONLY_THIS_PROCESS = 0x00000002,
            DEBUG_PROCESS = 0x00000001,
            DETACHED_PROCESS = 0x00000008,
            EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
            INHERIT_PARENT_AFFINITY = 0x00010000
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct STARTUPINFO
        {
            public uint cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LOAD_DLL_DEBUG_INFO
        {
            public IntPtr hFile;
            public IntPtr lpBaseOfDll;
            public UInt32 dwDebugInfoFileOffset;
            public UInt32 nDebugInfoSize;
            public IntPtr lpImageName;
            public UInt16 fUnicode;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UNLOAD_DLL_DEBUG_INFO
        {
            public IntPtr lpBaseOfDll;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EXIT_PROCESS_DEBUG_INFO
        {
            public UInt32 dwExitCode;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EXCEPTION_DEBUG_INFO
        {
            public EXCEPTION_RECORD ExceptionRecord;
            public bool dwFirstChance;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EXCEPTION_RECORD
        {
            public ExceptionCode ExceptionCode;
            public ExceptionFlags ExceptionFlags;

            public IntPtr ExceptionRecord;
            public IntPtr ExceptionAddress;

            public UInt32 NumberParameters;

            public IntPtr ExceptionInformation0;
            public IntPtr ExceptionInformation1;
            public IntPtr ExceptionInformation2;
            public IntPtr ExceptionInformation3;
            public IntPtr ExceptionInformation4;
            public IntPtr ExceptionInformation5;
            public IntPtr ExceptionInformation6;
            public IntPtr ExceptionInformation7;
            public IntPtr ExceptionInformation8;
            public IntPtr ExceptionInformation9;
            public IntPtr ExceptionInformation10;
            public IntPtr ExceptionInformation11;
            public IntPtr ExceptionInformation12;
            public IntPtr ExceptionInformation13;
            public IntPtr ExceptionInformation14;
        }

        //TODO
        public const uint STANDARD_RIGHTS_REQUIRED = (uint)(ThreadAccessRights.DELETE | ThreadAccessRights.READ_CONTROL | ThreadAccessRights.WRITE_DAC | ThreadAccessRights.WRITE_OWNER);

        public enum ACCESS_MASK : uint
        {
            DESKTOP_CREATEMENU = 0x4,
            DESKTOP_CREATEWINDOW = 0x2,
            DESKTOP_ENUMERATE = 0x40,
            DESKTOP_HOOKCONTROL = 0x8,
            DESKTOP_JOURNALPLAYBACK = 0x20,
            DESKTOP_JOURNALRECORD = 0x10,
            DESKTOP_READOBJECTS = 0x1,
            DESKTOP_SWITCHDESKTOP = 0x100,
            DESKTOP_WRITEOBJECTS = 0x80,

            GENERIC_ALL = (DESKTOP_CREATEMENU | DESKTOP_CREATEWINDOW | DESKTOP_ENUMERATE | DESKTOP_HOOKCONTROL | DESKTOP_JOURNALPLAYBACK | DESKTOP_JOURNALRECORD | DESKTOP_READOBJECTS | DESKTOP_SWITCHDESKTOP | DESKTOP_WRITEOBJECTS | STANDARD_RIGHTS_REQUIRED)
        }

        public enum WINDOW_MESSAGE : uint
        {
            BN_CLICKED = 0x0000,
            BM_CLICK = 0x00F5,
            WM_CLOSE = 0x0010,
            SC_CLOSE = 0xF060
        }

        public enum ContinueStatus : uint
        {
            DBG_CONTINUE = 0x00010002,
            DBG_EXCEPTION_NOT_HANDLED = 0x80010001
        }

        public enum SYSTEM_ERROR_CODE : uint
        {
            ERROR_SUCCESS = 0x0,
            ERROR_INVALID_FUNCTION = 0x1,
            ERROR_FILE_NOT_FOUND = 0x2,
            ERROR_PATH_NOT_FOUND = 0x3,
            ERROR_TOO_MANY_OPEN_FILES = 0x4,
            ERROR_ACCESS_DENIED = 0x5,
            ERROR_INVALID_HANDLE = 0x6,
            ERROR_ARENA_TRASHED = 0x7,
            ERROR_NOT_ENOUGH_MEMORY = 0x8,
            ERROR_INVALID_BLOCK = 0x9,
            ERROR_BAD_ENVIRONMENT = 0xA,
            ERROR_BAD_FORMAT = 0xB,
            ERROR_INVALID_ACCESS = 0xC,
            ERROR_INVALID_DATA = 0xD,
            ERROR_OUTOFMEMORY = 0xE,
            ERROR_INVALID_DRIVE = 0xF,
            ERROR_CURRENT_DIRECTORY = 0x10,
            ERROR_NOT_SAME_DEVICE = 0x11,
            ERROR_NO_MORE_FILES = 0x12,
            ERROR_WRITE_PROTECT = 0x13,
            ERROR_BAD_UNIT = 0x14,
            ERROR_NOT_READY = 0x15,
            ERROR_BAD_COMMAND = 0x16,
            ERROR_CRC = 0x17,
            ERROR_BAD_LENGTH = 0x18,
            ERROR_SEEK = 0x19,
            ERROR_NOT_DOS_DISK = 0x1A,
            ERROR_SECTOR_NOT_FOUND = 0x1B,
            ERROR_OUT_OF_PAPER = 0x1C,
            ERROR_WRITE_FAULT = 0x1D,
            ERROR_READ_FAULT = 0x1E,
            ERROR_GEN_FAILURE = 0x1F,
            ERROR_SHARING_VIOLATION = 0x20,
            ERROR_LOCK_VIOLATION = 0x21,
            ERROR_WRONG_DISK = 0x22,
            ERROR_SHARING_BUFFER_EXCEEDED = 0x24,
            ERROR_HANDLE_EOF = 0x26,
            ERROR_HANDLE_DISK_FULL = 0x27,
            ERROR_NOT_SUPPORTED = 0x32,
            ERROR_REM_NOT_LIST = 0x33,
            ERROR_DUP_NAME = 0x34,
            ERROR_BAD_NETPATH = 0x35,
            ERROR_NETWORK_BUSY = 0x36,
            ERROR_DEV_NOT_EXIST = 0x37,
            ERROR_TOO_MANY_CMDS = 0x38,
            ERROR_ADAP_HDW_ERR = 0x39,
            ERROR_BAD_NET_RESP = 0x3A,
            ERROR_UNEXP_NET_ERR = 0x3B,
            ERROR_BAD_REM_ADAP = 0x3C,
            ERROR_PRINTQ_FULL = 0x3D,
            ERROR_NO_SPOOL_SPACE = 0x3E,
            ERROR_PRINT_CANCELLED = 0x3F,
            ERROR_NETNAME_DELETED = 0x40,
            ERROR_NETWORK_ACCESS_DENIED = 0x41,
            ERROR_BAD_DEV_TYPE = 0x42,
            ERROR_BAD_NET_NAME = 0x43,
            ERROR_TOO_MANY_NAMES = 0x44,
            ERROR_TOO_MANY_SESS = 0x45,
            ERROR_SHARING_PAUSED = 0x46,
            ERROR_REQ_NOT_ACCEP = 0x47,
            ERROR_REDIR_PAUSED = 0x48,
            ERROR_FILE_EXISTS = 0x50,
            ERROR_CANNOT_MAKE = 0x52,
            ERROR_FAIL_I24 = 0x53,
            ERROR_OUT_OF_STRUCTURES = 0x54,
            ERROR_ALREADY_ASSIGNED = 0x55,
            ERROR_INVALID_PASSWORD = 0x56,
            ERROR_INVALID_PARAMETER = 0x57,
            ERROR_NET_WRITE_FAULT = 0x58,
            ERROR_NO_PROC_SLOTS = 0x59,
            ERROR_TOO_MANY_SEMAPHORES = 0x64,
            ERROR_EXCL_SEM_ALREADY_OWNED = 0x65,
            ERROR_SEM_IS_SET = 0x66,
            ERROR_TOO_MANY_SEM_REQUESTS = 0x67,
            ERROR_INVALID_AT_INTERRUPT_TIME = 0x68,
            ERROR_SEM_OWNER_DIED = 0x69,
            ERROR_SEM_USER_LIMIT = 0x6A,
            ERROR_DISK_CHANGE = 0x6B,
            ERROR_DRIVE_LOCKED = 0x6C,
            ERROR_BROKEN_PIPE = 0x6D,
            ERROR_OPEN_FAILED = 0x6E,
            ERROR_BUFFER_OVERFLOW = 0x6F,
            ERROR_DISK_FULL = 0x70,
            ERROR_NO_MORE_SEARCH_HANDLES = 0x71,
            ERROR_INVALID_TARGET_HANDLE = 0x72,
            ERROR_INVALID_CATEGORY = 0x75,
            ERROR_INVALID_VERIFY_SWITCH = 0x76,
            ERROR_BAD_DRIVER_LEVEL = 0x77,
            ERROR_CALL_NOT_IMPLEMENTED = 0x78,
            ERROR_SEM_TIMEOUT = 0x79,
            ERROR_INSUFFICIENT_BUFFER = 0x7A,
            ERROR_INVALID_NAME = 0x7B,
            ERROR_INVALID_LEVEL = 0x7C,
            ERROR_NO_VOLUME_LABEL = 0x7D,
            ERROR_MOD_NOT_FOUND = 0x7E,
            ERROR_PROC_NOT_FOUND = 0x7F,
            ERROR_WAIT_NO_CHILDREN = 0x80,
            ERROR_CHILD_NOT_COMPLETE = 0x81,
            ERROR_DIRECT_ACCESS_HANDLE = 0x82,
            ERROR_NEGATIVE_SEEK = 0x83,
            ERROR_SEEK_ON_DEVICE = 0x84,
            ERROR_IS_JOIN_TARGET = 0x85,
            ERROR_IS_JOINED = 0x86,
            ERROR_IS_SUBSTED = 0x87,
            ERROR_NOT_JOINED = 0x88,
            ERROR_NOT_SUBSTED = 0x89,
            ERROR_JOIN_TO_JOIN = 0x8A,
            ERROR_SUBST_TO_SUBST = 0x8B,
            ERROR_JOIN_TO_SUBST = 0x8C,
            ERROR_SUBST_TO_JOIN = 0x8D,
            ERROR_BUSY_DRIVE = 0x8E,
            ERROR_SAME_DRIVE = 0x8F,
            ERROR_DIR_NOT_ROOT = 0x90,
            ERROR_DIR_NOT_EMPTY = 0x91,
            ERROR_IS_SUBST_PATH = 0x92,
            ERROR_IS_JOIN_PATH = 0x93,
            ERROR_PATH_BUSY = 0x94,
            ERROR_IS_SUBST_TARGET = 0x95,
            ERROR_SYSTEM_TRACE = 0x96,
            ERROR_INVALID_EVENT_COUNT = 0x97,
            ERROR_TOO_MANY_MUXWAITERS = 0x98,
            ERROR_INVALID_LIST_FORMAT = 0x99,
            ERROR_LABEL_TOO_LONG = 0x9A,
            ERROR_TOO_MANY_TCBS = 0x9B,
            ERROR_SIGNAL_REFUSED = 0x9C,
            ERROR_DISCARDED = 0x9D,
            ERROR_NOT_LOCKED = 0x9E,
            ERROR_BAD_THREADID_ADDR = 0x9F,
            ERROR_BAD_ARGUMENTS = 0xA0,
            ERROR_BAD_PATHNAME = 0xA1,
            ERROR_SIGNAL_PENDING = 0xA2,
            ERROR_MAX_THRDS_REACHED = 0xA4,
            ERROR_LOCK_FAILED = 0xA7,
            ERROR_BUSY = 0xAA,
            ERROR_DEVICE_SUPPORT_IN_PROGRESS = 0xAB,
            ERROR_CANCEL_VIOLATION = 0xAD,
            ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 0xAE,
            ERROR_INVALID_SEGMENT_NUMBER = 0xB4,
            ERROR_INVALID_ORDINAL = 0xB6,
            ERROR_ALREADY_EXISTS = 0xB7,
            ERROR_INVALID_FLAG_NUMBER = 0xBA,
            ERROR_SEM_NOT_FOUND = 0xBB,
            ERROR_INVALID_STARTING_CODESEG = 0xBC,
            ERROR_INVALID_STACKSEG = 0xBD,
            ERROR_INVALID_MODULETYPE = 0xBE,
            ERROR_INVALID_EXE_SIGNATURE = 0xBF,
            ERROR_EXE_MARKED_INVALID = 0xC0,
            ERROR_BAD_EXE_FORMAT = 0xC1,
            ERROR_ITERATED_DATA_EXCEEDS_64k = 0xC2,
            ERROR_INVALID_MINALLOCSIZE = 0xC3,
            ERROR_DYNLINK_FROM_INVALID_RING = 0xC4,
            ERROR_IOPL_NOT_ENABLED = 0xC5,
            ERROR_INVALID_SEGDPL = 0xC6,
            ERROR_AUTODATASEG_EXCEEDS_64k = 0xC7,
            ERROR_RING2SEG_MUST_BE_MOVABLE = 0xC8,
            ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 0xC9,
            ERROR_INFLOOP_IN_RELOC_CHAIN = 0xCA,
            ERROR_ENVVAR_NOT_FOUND = 0xCB,
            ERROR_NO_SIGNAL_SENT = 0xCD,
            ERROR_FILENAME_EXCED_RANGE = 0xCE,
            ERROR_RING2_STACK_IN_USE = 0xCF,
            ERROR_META_EXPANSION_TOO_LONG = 0xD0,
            ERROR_INVALID_SIGNAL_NUMBER = 0xD1,
            ERROR_THREAD_1_INACTIVE = 0xD2,
            ERROR_LOCKED = 0xD4,
            ERROR_TOO_MANY_MODULES = 0xD6,
            ERROR_NESTING_NOT_ALLOWED = 0xD7,
            ERROR_EXE_MACHINE_TYPE_MISMATCH = 0xD8,
            ERROR_EXE_CANNOT_MODIFY_SIGNED_BINARY = 0xD9,
            ERROR_EXE_CANNOT_MODIFY_STRONG_SIGNED_BINARY = 0xDA,
            ERROR_FILE_CHECKED_OUT = 0xDC,
            ERROR_CHECKOUT_REQUIRED = 0xDD,
            ERROR_BAD_FILE_TYPE = 0xDE,
            ERROR_FILE_TOO_LARGE = 0xDF,
            ERROR_FORMS_AUTH_REQUIRED = 0xE0,
            ERROR_VIRUS_INFECTED = 0xE1,
            ERROR_VIRUS_DELETED = 0xE2,
            ERROR_PIPE_LOCAL = 0xE5,
            ERROR_BAD_PIPE = 0xE6,
            ERROR_PIPE_BUSY = 0xE7,
            ERROR_NO_DATA = 0xE8,
            ERROR_PIPE_NOT_CONNECTED = 0xE9,
            ERROR_MORE_DATA = 0xEA,
            ERROR_VC_DISCONNECTED = 0xF0,
            ERROR_INVALID_EA_NAME = 0xFE,
            ERROR_EA_LIST_INCONSISTENT = 0xFF,
            WAIT_TIMEOUT = 0x102,
            ERROR_NO_MORE_ITEMS = 0x103,
            ERROR_CANNOT_COPY = 0x10A,
            ERROR_DIRECTORY = 0x10B,
            ERROR_EAS_DIDNT_FIT = 0x113,
            ERROR_EA_FILE_CORRUPT = 0x114,
            ERROR_EA_TABLE_FULL = 0x115,
            ERROR_INVALID_EA_HANDLE = 0x116,
            ERROR_EAS_NOT_SUPPORTED = 0x11A,
            ERROR_NOT_OWNER = 0x120,
            ERROR_TOO_MANY_POSTS = 0x12A,
            ERROR_PARTIAL_COPY = 0x12B,
            ERROR_OPLOCK_NOT_GRANTED = 0x12C,
            ERROR_INVALID_OPLOCK_PROTOCOL = 0x12D,
            ERROR_DISK_TOO_FRAGMENTED = 0x12E,
            ERROR_DELETE_PENDING = 0x12F,
            ERROR_INCOMPATIBLE_WITH_GLOBAL_SHORT_NAME_REGISTRY_SETTING = 0x130,
            ERROR_SHORT_NAMES_NOT_ENABLED_ON_VOLUME = 0x131,
            ERROR_SECURITY_STREAM_IS_INCONSISTENT = 0x132,
            ERROR_INVALID_LOCK_RANGE = 0x133,
            ERROR_IMAGE_SUBSYSTEM_NOT_PRESENT = 0x134,
            ERROR_NOTIFICATION_GUID_ALREADY_DEFINED = 0x135,
            ERROR_INVALID_EXCEPTION_HANDLER = 0x136,
            ERROR_DUPLICATE_PRIVILEGES = 0x137,
            ERROR_NO_RANGES_PROCESSED = 0x138,
            ERROR_NOT_ALLOWED_ON_SYSTEM_FILE = 0x139,
            ERROR_DISK_RESOURCES_EXHAUSTED = 0x13A,
            ERROR_INVALID_TOKEN = 0x13B,
            ERROR_DEVICE_FEATURE_NOT_SUPPORTED = 0x13C,
            ERROR_MR_MID_NOT_FOUND = 0x13D,
            ERROR_SCOPE_NOT_FOUND = 0x13E,
            ERROR_UNDEFINED_SCOPE = 0x13F,
            ERROR_INVALID_CAP = 0x140,
            ERROR_DEVICE_UNREACHABLE = 0x141,
            ERROR_DEVICE_NO_RESOURCES = 0x142,
            ERROR_DATA_CHECKSUM_ERROR = 0x143,
            ERROR_INTERMIXED_SECURE_EA_OPERATION = 0x144,
            ERROR_SPECIFIED_COPY_READ = 0x145,
            ERROR_REPAIR_DEFERRED = 0x146,
            ERROR_OFFSET_ALIGNMENT_VIOLATION = 0x147,
            ERROR_INVALID_FIELD_IN_PARAMETER_LIST = 0x148,
            ERROR_OPERATION_IN_PROGRESS = 0x149,
            ERROR_BAD_DEVICE_PATH = 0x14A,
            ERROR_TOO_MANY_DESCRIPTORS = 0x14B,
            ERROR_SCRUB_DATA_DISABLED = 0x14C,
            ERROR_FAIL_NOACTION_REBOOT = 0x15E,
            ERROR_FAIL_SHUTDOWN = 0x15F,
            ERROR_FAIL_RESTART = 0x160,
            ERROR_MAX_SESSIONS_REACHED = 0x161,
            ERROR_THREAD_MODE_ALREADY_BACKGROUND = 0x190,
            ERROR_THREAD_MODE_NOT_BACKGROUND = 0x191,
            ERROR_PROCESS_MODE_ALREADY_BACKGROUND = 0x192,
            ERROR_PROCESS_MODE_NOT_BACKGROUND = 0x193,
            ERROR_INVALID_ADDRESS = 0x1E7
        }

        public enum ExceptionCode : uint
        {
            None = 0x0,
            STATUS_BREAKPOINT = 0x80000003,
            STATUS_SINGLESTEP = 0x80000004,
            STATUS_ENTRYPOINT_NOT_FOUND = 0xC0000139,
            EXCEPTION_DATATYPE_MISALIGNMENT = 0x80000002,
            EXCEPTION_SINGLE_STEP = 0x80000004,
            EXCEPTION_INT_DIVIDE_BY_ZERO = 0xC0000094,
            EXCEPTION_BREAKPOINT = 0x80000003,
            EXCEPTION_STACK_OVERFLOW = 0xC00000FD,
            EXCEPTION_NONCONTINUABLE_EXCEPTION = 0xC0000025,
            EXCEPTION_ACCESS_VIOLATION = 0xc0000005,

            EXCEPTION_ARRAY_BOUNDS_EXCEEDED = 0xC000008C,

            //TODO: check if they also belong here. Couldn't find 100% evidence, yet. But the upper ones seem to be correct, though.
            EXCEPTION_FLT_DENORMAL_OPERAND,
            EXCEPTION_FLT_DIVIDE_BY_ZERO,
            EXCEPTION_FLT_INEXACT_RESULT,
            EXCEPTION_FLT_INVALID_OPERATION,
            EXCEPTION_FLT_OVERFLOW,
            EXCEPTION_FLT_STACK_CHECK,
            EXCEPTION_FLT_UNDERFLOW,
            EXCEPTION_GUARD_PAGE,
            EXCEPTION_ILLEGAL_INSTRUCTION,
            EXCEPTION_IN_PAGE_ERROR,
            EXCEPTION_INT_OVERFLOW,
            EXCEPTION_INVALID_DISPOSITION,
            EXCEPTION_INVALID_HANDLE,
            EXCEPTION_PRIV_INSTRUCTION,
            STATUS_UNWIND_CONSOLIDATE
        }

        [Flags]
        public enum ExceptionFlags : uint
        {
            None = 0x0,
            EXCEPTION_NONCONTINUABLE = 0x1,
        }

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool GetThreadContext(IntPtr hThread, ref CONTEXT lpContext);

        [Flags]
        public enum CONTEXT64_FLAGS : uint
        {
            CONTEXT64_AMD64 = 0x100000,
            CONTEXT64_CONTROL = CONTEXT64_AMD64 | 0x01,
            CONTEXT64_INTEGER = CONTEXT64_AMD64 | 0x02,
            CONTEXT64_SEGMENTS = CONTEXT64_AMD64 | 0x04,
            CONTEXT64_FLOATING_POINT = CONTEXT64_AMD64 | 0x08,
            CONTEXT64_DEBUG_REGISTERS = CONTEXT64_AMD64 | 0x10,
            CONTEXT64_FULL = CONTEXT64_CONTROL | CONTEXT64_INTEGER | CONTEXT64_FLOATING_POINT,
            CONTEXT64_ALL = CONTEXT64_CONTROL | CONTEXT64_INTEGER | CONTEXT64_SEGMENTS | CONTEXT64_FLOATING_POINT | CONTEXT64_DEBUG_REGISTERS
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CONTEXT
        {
            public CONTEXT_FLAGS ContextFlags;

            public uint Dr0;
            public uint Dr1;
            public uint Dr2;
            public uint Dr3;
            public uint Dr6;
            public uint Dr7;

            public FLOATING_SAVE_AREA FloatSave;

            public uint SegGs;
            public uint SegFs;
            public uint SegEs;
            public uint SegDs;

            public uint Edi;
            public uint Esi;
            public uint Ebx;
            public uint Edx;
            public uint Ecx;
            public uint Eax;

            public uint Ebp;
            public uint Eip;
            public uint SegCs;
            public uint EFlags;
            public uint Esp;
            public uint SegSs;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] ExtendedRegisters;
        }

        public enum CONTEXT_FLAGS : uint
        {
            CONTEXT_i386 = 0x10000,
            CONTEXT_i486 = 0x10000,
            CONTEXT_CONTROL = CONTEXT_i386 | 0x01,
            CONTEXT_INTEGER = CONTEXT_i386 | 0x02,
            CONTEXT_SEGMENTS = CONTEXT_i386 | 0x04,
            CONTEXT_FLOATING_POINT = CONTEXT_i386 | 0x08,
            CONTEXT_DEBUG_REGISTERS = CONTEXT_i386 | 0x10,
            CONTEXT_EXTENDED_REGISTERS = CONTEXT_i386 | 0x20,
            CONTEXT_FULL = CONTEXT_CONTROL |
                            CONTEXT_INTEGER |
                            CONTEXT_SEGMENTS,
            CONTEXT_ALL = CONTEXT_CONTROL |
                            CONTEXT_INTEGER |
                            CONTEXT_SEGMENTS |
                            CONTEXT_FLOATING_POINT |
                            CONTEXT_DEBUG_REGISTERS |
                            CONTEXT_EXTENDED_REGISTERS
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FLOATING_SAVE_AREA
        {
            public uint ControlWord;
            public uint StatusWord;
            public uint TagWord;
            public uint ErrorOffset;
            public uint ErrorSelector;
            public uint DataOffset;
            public uint DataSelector;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] RegisterArea;
            public uint Cr0NpxState;
        }

        [Flags]
        public enum KBDLLHOOKSTRUCTFlags : uint
        {
            LLKHF_EXTENDED = 0x01,
            LLKHF_INJECTED = 0x10,
            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }

        [System.Flags]
        public enum LoadLibraryFlags : uint
        {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }

        [DllImport("user32.dll")]
        public static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);

        [StructLayout(LayoutKind.Sequential)]
        public struct COMBOBOXINFO
        {
            public Int32 cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public ComboBoxButtonState buttonState;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }

        public enum ComboBoxButtonState
        {
            STATE_SYSTEM_NONE = 0,
            STATE_SYSTEM_INVISIBLE = 0x00008000,
            STATE_SYSTEM_PRESSED = 0x00000008
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public static readonly int GWL_EXSTYLE = (-20);
        // Window Styles 
        const UInt32 WS_OVERLAPPED = 0;
        const UInt32 WS_POPUP = 0x80000000;
        const UInt32 WS_CHILD = 0x40000000;
        const UInt32 WS_MINIMIZE = 0x20000000;
        const UInt32 WS_VISIBLE = 0x10000000;
        const UInt32 WS_DISABLED = 0x8000000;
        const UInt32 WS_CLIPSIBLINGS = 0x4000000;
        const UInt32 WS_CLIPCHILDREN = 0x2000000;
        const UInt32 WS_MAXIMIZE = 0x1000000;
        const UInt32 WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
        const UInt32 WS_BORDER = 0x800000;
        const UInt32 WS_DLGFRAME = 0x400000;
        const UInt32 WS_VSCROLL = 0x200000;
        const UInt32 WS_HSCROLL = 0x100000;
        const UInt32 WS_SYSMENU = 0x80000;
        const UInt32 WS_THICKFRAME = 0x40000;
        const UInt32 WS_GROUP = 0x20000;
        const UInt32 WS_TABSTOP = 0x10000;
        const UInt32 WS_MINIMIZEBOX = 0x20000;
        const UInt32 WS_MAXIMIZEBOX = 0x10000;
        const UInt32 WS_TILED = WS_OVERLAPPED;
        const UInt32 WS_ICONIC = WS_MINIMIZE;
        const UInt32 WS_SIZEBOX = WS_THICKFRAME;

        // Extended Window Styles 
        const UInt32 WS_EX_DLGMODALFRAME = 0x0001;
        const UInt32 WS_EX_NOPARENTNOTIFY = 0x0004;
        public const UInt32 WS_EX_TOPMOST = 0x0008;
        const UInt32 WS_EX_ACCEPTFILES = 0x0010;
        const UInt32 WS_EX_TRANSPARENT = 0x0020;
        const UInt32 WS_EX_MDICHILD = 0x0040;
        const UInt32 WS_EX_TOOLWINDOW = 0x0080;
        const UInt32 WS_EX_WINDOWEDGE = 0x0100;
        const UInt32 WS_EX_CLIENTEDGE = 0x0200;
        const UInt32 WS_EX_CONTEXTHELP = 0x0400;
        const UInt32 WS_EX_RIGHT = 0x1000;
        const UInt32 WS_EX_LEFT = 0x0000;
        const UInt32 WS_EX_RTLREADING = 0x2000;
        const UInt32 WS_EX_LTRREADING = 0x0000;
        const UInt32 WS_EX_LEFTSCROLLBAR = 0x4000;
        const UInt32 WS_EX_RIGHTSCROLLBAR = 0x0000;
        const UInt32 WS_EX_CONTROLPARENT = 0x10000;
        const UInt32 WS_EX_STATICEDGE = 0x20000;
        const UInt32 WS_EX_APPWINDOW = 0x40000;
        const UInt32 WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        const UInt32 WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        const UInt32 WS_EX_LAYERED = 0x00080000;
        const UInt32 WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
        const UInt32 WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
        const UInt32 WS_EX_COMPOSITED = 0x02000000;
        const UInt32 WS_EX_NOACTIVATE = 0x08000000;

        [StructLayout(LayoutKind.Sequential)]
        public struct MENUITEMINFO
        {
            public uint cbSize;
            public uint fMask;
            public uint fType;
            public uint fState;
            public int wID;
            public int hSubMenu;
            public int hbmpChecked;
            public int hbmpUnchecked;
            public int dwItemData;
            public string dwTypeData;
            public uint cch;
            public int hbmpItem;
        }

        public enum GWL
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }

        // Values for the fMask parameter
        //From winuser.h
        public const UInt32 MIM_MAXHEIGHT = 0x00000001;
        public const UInt32 MIM_BACKGROUND = 0x00000002;
        public const UInt32 MIM_HELPID = 0x00000004;
        public const UInt32 MIM_MENUDATA = 0x00000008;
        public const UInt32 MIM_STYLE = 0x00000010;
        public const UInt32 MIM_APPLYTOSUBMENUS = 0x80000000;

        public enum DESKTOP_ACCESS : uint
        {
            DESKTOP_NONE = 0,
            DESKTOP_READOBJECTS = 0x0001,
            DESKTOP_CREATEWINDOW = 0x0002,
            DESKTOP_CREATEMENU = 0x0004,
            DESKTOP_HOOKCONTROL = 0x0008,
            DESKTOP_JOURNALRECORD = 0x0010,
            DESKTOP_JOURNALPLAYBACK = 0x0020,
            DESKTOP_ENUMERATE = 0x0040,
            DESKTOP_WRITEOBJECTS = 0x0080,
            DESKTOP_SWITCHDESKTOP = 0x0100,

            GENERIC_ALL = (DESKTOP_READOBJECTS | DESKTOP_CREATEWINDOW | DESKTOP_CREATEMENU |
                            DESKTOP_HOOKCONTROL | DESKTOP_JOURNALRECORD | DESKTOP_JOURNALPLAYBACK |
                            DESKTOP_ENUMERATE | DESKTOP_WRITEOBJECTS | DESKTOP_SWITCHDESKTOP),
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags, uint dwDesiredAccess, IntPtr lpsa);

        [DllImport("user32.dll")]
        public static extern bool CloseDesktop(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(int dwThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("user32.dll")]
        public static extern int GetWindowWord(int hwnd, long nIndex);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr LoadLibrary([In] string LibraryName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, int nSize);

        //[DllImport("psapi.dll", SetLastError = true)]
        //public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpFilename, [MarshalAs(UnmanagedType.U4)] int nSize);


        [DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        //* only needed if your application is using a dialog box and needs to 
        //* respond to a "QueryCancelAutoPlay" message, it cannot simply return TRUE or FALSE.
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)] //
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam,
        int lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr hwnd, int Msg, IntPtr wParam, IntPtr lParam, int flags, int uTimeout, out IntPtr pResult);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(HandleRef hWnd);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr hwnd, int Msg, IntPtr wParam, ref MINMAXINFO lParam, int flags, int uTimeout, out IntPtr pResult);

        // Overload for use with WM_GETTEXT. StringBuilder is the type that P/Invoke maps to an output TCHAR*, see
        // see MSDN .Net docs on P/Invoke for more details. 
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr hwnd, int Msg, IntPtr wParam, StringBuilder lParam, int flags, int uTimeout, out IntPtr pResult);

        [StructLayout(LayoutKind.Explicit)]
        public struct MOUSE_EVENT_RECORD
        {
            [FieldOffset(0)]
            public POINT dwMousePosition;   //COORD = POINT?! TODO!
            [FieldOffset(4)]
            public uint dwButtonState;
            [FieldOffset(8)]
            public uint dwControlKeyState;
            [FieldOffset(12)]
            public uint dwEventFlags;
        }

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
           UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
          int dwExtraInfo);
        //Use the values of this enum for the 'dwData' parameter
        //to specify an X button when using MouseEventFlags.XDOWN or
        //MouseEventFlags.XUP for the dwFlags parameter.
        public enum MouseEventDataXButtons : uint
        {
            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }


        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int SendMessageGetTextLength(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static IntPtr SendMessageGetText(IntPtr hWnd, int msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        //[DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern bool ReadProcessMemory([In] IntPtr ProcessHandle, [In] IntPtr BaseAddress, [In, Out] Byte[] Buffer, [In] int BufferSize, [Out] out int BytesRead);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool WriteProcessMemory([In] IntPtr ProcessHandle, [In] IntPtr BaseAddress, [In] byte[] Buffer, [In] int BufferSize, [Out] out int BytesWritten);

        [DllImport("kernel32.dll")]
        public static extern int GetProcessId(IntPtr hProcess);

        [DllImport("kernel32.dll")]
        public static extern void OutputDebugStringA(byte[] byteASCII);
        [DllImport("kernel32.dll")]
        public static extern void OutputDebugStringW(byte[] byteUnicode);

        [DllImport("kernel32")]
        public static extern IntPtr CloseHandle(IntPtr handle);
        public struct OUTPUT_DEBUG_STRING_INFO
        {
            public IntPtr lpDebugStringData;
            public ushort fUnicode;
            public ushort nDebugStringLength;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TCITEM
        {
            public uint mask;
            public int state;
            public int statemask;
            public IntPtr text;
            public int size;
            public int image;
            public int param;
        }

        [Flags]
        public enum ProcessAccess
        {
            /// <summary>
            /// Required to create a thread.
            /// </summary>
            CreateThread = 0x0002,

            /// <summary>
            /// 
            /// </summary>
            SetSessionId = 0x0004,

            /// <summary>
            /// Required to perform an operation on the address space of a process 
            /// </summary>
            VmOperation = 0x0008,

            /// <summary>
            /// Required to read memory in a process using ReadProcessMemory.
            /// </summary>
            VmRead = 0x0010,

            /// <summary>
            /// Required to write to memory in a process using WriteProcessMemory.
            /// </summary>
            VmWrite = 0x0020,

            /// <summary>
            /// Required to duplicate a handle using DuplicateHandle.
            /// </summary>
            DupHandle = 0x0040,

            /// <summary>
            /// Required to create a process.
            /// </summary>
            CreateProcess = 0x0080,

            /// <summary>
            /// Required to set memory limits using SetProcessWorkingSetSize.
            /// </summary>
            SetQuota = 0x0100,

            /// <summary>
            /// Required to set certain information about a process, such as its priority class (see SetPriorityClass).
            /// </summary>
            SetInformation = 0x0200,

            /// <summary>
            /// Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).
            /// </summary>
            QueryInformation = 0x0400,

            /// <summary>
            /// Required to suspend or resume a process.
            /// </summary>
            SuspendResume = 0x0800,

            /// <summary>
            /// Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName). 
            /// A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION.
            /// </summary>
            QueryLimitedInformation = 0x1000,

            /// <summary>
            /// Required to wait for the process to terminate using the wait functions.
            /// </summary>
            Synchronize = 0x100000,

            /// <summary>
            /// Required to delete the object.
            /// </summary>
            Delete = 0x00010000,

            /// <summary>
            /// Required to read information in the security descriptor for the object, not including the information in the SACL. 
            /// To read or write the SACL, you must request the ACCESS_SYSTEM_SECURITY access right. For more information, see SACL Access Right.
            /// </summary>
            ReadControl = 0x00020000,

            /// <summary>
            /// Required to modify the DACL in the security descriptor for the object.
            /// </summary>
            WriteDac = 0x00040000,

            /// <summary>
            /// Required to change the owner in the security descriptor for the object.
            /// </summary>
            WriteOwner = 0x00080000,

            StandardRightsRequired = 0x000F0000,

            /// <summary>
            /// All possible access rights for a process object.
            /// </summary>
            AllAccess = StandardRightsRequired | Synchronize | 0xFFFF
        }

        /// <summary>
        /// Windows Messages
        /// Defined in winuser.h from Windows SDK v6.1
        /// Documentation pulled from MSDN.
        /// </summary>
        public enum WM : uint
        {
            /// <summary>
            /// The WM_NULL message performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
            /// </summary>
            NULL = 0x0000,
            /// <summary>
            /// The WM_CREATE message is sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created, but before the window becomes visible.
            /// </summary>
            CREATE = 0x0001,
            /// <summary>
            /// The WM_DESTROY message is sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen. 
            /// This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message, it can be assumed that all child windows still exist.
            /// /// </summary>
            DESTROY = 0x0002,
            /// <summary>
            /// The WM_MOVE message is sent after a window has been moved. 
            /// </summary>
            MOVE = 0x0003,
            /// <summary>
            /// The WM_SIZE message is sent to a window after its size has changed.
            /// </summary>
            SIZE = 0x0005,
            /// <summary>
            /// The WM_ACTIVATE message is sent to both the window being activated and the window being deactivated. If the windows use the same input queue, the message is sent synchronously, first to the window procedure of the top-level window being deactivated, then to the window procedure of the top-level window being activated. If the windows use different input queues, the message is sent asynchronously, so the window is activated immediately. 
            /// </summary>
            ACTIVATE = 0x0006,
            /// <summary>
            /// The WM_SETFOCUS message is sent to a window after it has gained the keyboard focus. 
            /// </summary>
            SETFOCUS = 0x0007,
            /// <summary>
            /// The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus. 
            /// </summary>
            KILLFOCUS = 0x0008,
            /// <summary>
            /// The WM_ENABLE message is sent when an application changes the enabled state of a window. It is sent to the window whose enabled state is changing. This message is sent before the EnableWindow function returns, but after the enabled state (WS_DISABLED style bit) of the window has changed. 
            /// </summary>
            ENABLE = 0x000A,
            /// <summary>
            /// An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn. 
            /// </summary>
            SETREDRAW = 0x000B,
            /// <summary>
            /// An application sends a WM_SETTEXT message to set the text of a window. 
            /// </summary>
            SETTEXT = 0x000C,
            /// <summary>
            /// An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller. 
            /// </summary>
            GETTEXT = 0x000D,
            /// <summary>
            /// An application sends a WM_GETTEXTLENGTH message to determine the length, in characters, of the text associated with a window. 
            /// </summary>
            GETTEXTLENGTH = 0x000E,
            /// <summary>
            /// The WM_PAINT message is sent when the system or another application makes a request to paint a portion of an application's window. The message is sent when the UpdateWindow or RedrawWindow function is called, or by the DispatchMessage function when the application obtains a WM_PAINT message by using the GetMessage or PeekMessage function. 
            /// </summary>
            PAINT = 0x000F,
            /// <summary>
            /// The WM_CLOSE message is sent as a signal that a window or an application should terminate.
            /// </summary>
            CLOSE = 0x0010,
            /// <summary>
            /// The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero, the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero.
            /// After processing this message, the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.
            /// </summary>
            QUERYENDSESSION = 0x0011,
            /// <summary>
            /// The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.
            /// </summary>
            QUERYOPEN = 0x0013,
            /// <summary>
            /// The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.
            /// </summary>
            ENDSESSION = 0x0016,
            /// <summary>
            /// The WM_QUIT message indicates a request to terminate an application and is generated when the application calls the PostQuitMessage function. It causes the GetMessage function to return zero.
            /// </summary>
            QUIT = 0x0012,
            /// <summary>
            /// The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized). The message is sent to prepare an invalidated portion of a window for painting. 
            /// </summary>
            ERASEBKGND = 0x0014,
            /// <summary>
            /// This message is sent to all top-level windows when a change is made to a system color setting. 
            /// </summary>
            SYSCOLORCHANGE = 0x0015,
            /// <summary>
            /// The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown.
            /// </summary>
            SHOWWINDOW = 0x0018,
            /// <summary>
            /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
            /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
            /// </summary>
            WININICHANGE = 0x001A,
            /// <summary>
            /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
            /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
            /// </summary>
            SETTINGCHANGE = WM.WININICHANGE,
            /// <summary>
            /// The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings. 
            /// </summary>
            DEVMODECHANGE = 0x001B,
            /// <summary>
            /// The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated. The message is sent to the application whose window is being activated and to the application whose window is being deactivated.
            /// </summary>
            ACTIVATEAPP = 0x001C,
            /// <summary>
            /// An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources. 
            /// </summary>
            FONTCHANGE = 0x001D,
            /// <summary>
            /// A message that is sent whenever there is a change in the system time.
            /// </summary>
            TIMECHANGE = 0x001E,
            /// <summary>
            /// The WM_CANCELMODE message is sent to cancel certain modes, such as mouse capture. For example, the system sends this message to the active window when a dialog box or message box is displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example, the EnableWindow function sends this message when disabling the specified window.
            /// </summary>
            CANCELMODE = 0x001F,
            /// <summary>
            /// The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured. 
            /// </summary>
            SETCURSOR = 0x0020,
            /// <summary>
            /// The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.
            /// </summary>
            MOUSEACTIVATE = 0x0021,
            /// <summary>
            /// The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.
            /// </summary>
            CHILDACTIVATE = 0x0022,
            /// <summary>
            /// The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure. 
            /// </summary>
            QUEUESYNC = 0x0023,
            /// <summary>
            /// The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change. An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size. 
            /// </summary>
            GETMINMAXINFO = 0x0024,
            /// <summary>
            /// Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows, except in unusual circumstances explained in the Remarks.
            /// </summary>
            PAINTICON = 0x0026,
            /// <summary>
            /// Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.
            /// </summary>
            ICONERASEBKGND = 0x0027,
            /// <summary>
            /// The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box. 
            /// </summary>
            NEXTDLGCTL = 0x0028,
            /// <summary>
            /// The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue. 
            /// </summary>
            SPOOLERSTATUS = 0x002A,
            /// <summary>
            /// The WM_DRAWITEM message is sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the button, combo box, list box, or menu has changed.
            /// </summary>
            DRAWITEM = 0x002B,
            /// <summary>
            /// The WM_MEASUREITEM message is sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.
            /// </summary>
            MEASUREITEM = 0x002C,
            /// <summary>
            /// Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.
            /// </summary>
            DELETEITEM = 0x002D,
            /// <summary>
            /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message. 
            /// </summary>
            VKEYTOITEM = 0x002E,
            /// <summary>
            /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message. 
            /// </summary>
            CHARTOITEM = 0x002F,
            /// <summary>
            /// An application sends a WM_SETFONT message to specify the font that a control is to use when drawing text. 
            /// </summary>
            SETFONT = 0x0030,
            /// <summary>
            /// An application sends a WM_GETFONT message to a control to retrieve the font with which the control is currently drawing its text. 
            /// </summary>
            GETFONT = 0x0031,
            /// <summary>
            /// An application sends a WM_SETHOTKEY message to a window to associate a hot key with the window. When the user presses the hot key, the system activates the window. 
            /// </summary>
            SETHOTKEY = 0x0032,
            /// <summary>
            /// An application sends a WM_GETHOTKEY message to determine the hot key associated with a window. 
            /// </summary>
            GETHOTKEY = 0x0033,
            /// <summary>
            /// The WM_QUERYDRAGICON message is sent to a minimized (iconic) window. The window is about to be dragged by the user but does not have an icon defined for its class. An application can return a handle to an icon or cursor. The system displays this cursor or icon while the user drags the icon.
            /// </summary>
            QUERYDRAGICON = 0x0037,
            /// <summary>
            /// The system sends the WM_COMPAREITEM message to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box. Whenever the application adds a new item, the system sends this message to the owner of a combo box or list box created with the CBS_SORT or LBS_SORT style. 
            /// </summary>
            COMPAREITEM = 0x0039,
            /// <summary>
            /// Active Accessibility sends the WM_GETOBJECT message to obtain information about an accessible object contained in a server application. 
            /// Applications never send this message directly. It is sent only by Active Accessibility in response to calls to AccessibleObjectFromPoint, AccessibleObjectFromEvent, or AccessibleObjectFromWindow. However, server applications handle this message. 
            /// </summary>
            GETOBJECT = 0x003D,
            /// <summary>
            /// The WM_COMPACTING message is sent to all top-level windows when the system detects more than 12.5 percent of system time over a 30- to 60-second interval is being spent compacting memory. This indicates that system memory is low.
            /// </summary>
            COMPACTING = 0x0041,
            /// <summary>
            /// WM_COMMNOTIFY is Obsolete for Win32-Based Applications
            /// </summary>
            [Obsolete]
            COMMNOTIFY = 0x0044,
            /// <summary>
            /// The WM_WINDOWPOSCHANGING message is sent to a window whose size, position, or place in the Z order is about to change as a result of a call to the SetWindowPos function or another window-management function.
            /// </summary>
            WINDOWPOSCHANGING = 0x0046,
            /// <summary>
            /// The WM_WINDOWPOSCHANGED message is sent to a window whose size, position, or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
            /// </summary>
            WINDOWPOSCHANGED = 0x0047,
            /// <summary>
            /// Notifies applications that the system, typically a battery-powered personal computer, is about to enter a suspended mode.
            /// Use: POWERBROADCAST
            /// </summary>
            [Obsolete]
            POWER = 0x0048,
            /// <summary>
            /// An application sends the WM_COPYDATA message to pass data to another application. 
            /// </summary>
            COPYDATA = 0x004A,
            /// <summary>
            /// The WM_CANCELJOURNAL message is posted to an application when a user cancels the application's journaling activities. The message is posted with a NULL window handle. 
            /// </summary>
            CANCELJOURNAL = 0x004B,
            /// <summary>
            /// Sent by a common control to its parent window when an event has occurred or the control requires some information. 
            /// </summary>
            NOTIFY = 0x004E,
            /// <summary>
            /// The WM_INPUTLANGCHANGEREQUEST message is posted to the window with the focus when the user chooses a new input language, either with the hotkey (specified in the Keyboard control panel application) or from the indicator on the system taskbar. An application can accept the change by passing the message to the DefWindowProc function or reject the change (and prevent it from taking place) by returning immediately. 
            /// </summary>
            INPUTLANGCHANGEREQUEST = 0x0050,
            /// <summary>
            /// The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function, which passes the message to all first-level child windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows, and so on. 
            /// </summary>
            INPUTLANGCHANGE = 0x0051,
            /// <summary>
            /// Sent to an application that has initiated a training card with Microsoft Windows Help. The message informs the application when the user clicks an authorable button. An application initiates a training card by specifying the HELP_TCARD command in a call to the WinHelp function.
            /// </summary>
            TCARD = 0x0052,
            /// <summary>
            /// Indicates that the user pressed the F1 key. If a menu is active when F1 is pressed, WM_HELP is sent to the window associated with the menu; otherwise, WM_HELP is sent to the window that has the keyboard focus. If no window has the keyboard focus, WM_HELP is sent to the currently active window. 
            /// </summary>
            HELP = 0x0053,
            /// <summary>
            /// The WM_USERCHANGED message is sent to all windows after the user has logged on or off. When the user logs on or off, the system updates the user-specific settings. The system sends this message immediately after updating the settings.
            /// </summary>
            USERCHANGED = 0x0054,
            /// <summary>
            /// Determines if a window accepts ANSI or Unicode structures in the WM_NOTIFY notification message. WM_NOTIFYFORMAT messages are sent from a common control to its parent window and from the parent window to the common control.
            /// </summary>
            NOTIFYFORMAT = 0x0055,
            /// <summary>
            /// The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.
            /// </summary>
            CONTEXTMENU = 0x007B,
            /// <summary>
            /// The WM_STYLECHANGING message is sent to a window when the SetWindowLong function is about to change one or more of the window's styles.
            /// </summary>
            STYLECHANGING = 0x007C,
            /// <summary>
            /// The WM_STYLECHANGED message is sent to a window after the SetWindowLong function has changed one or more of the window's styles
            /// </summary>
            STYLECHANGED = 0x007D,
            /// <summary>
            /// The WM_DISPLAYCHANGE message is sent to all windows when the display resolution has changed.
            /// </summary>
            DISPLAYCHANGE = 0x007E,
            /// <summary>
            /// The WM_GETICON message is sent to a window to retrieve a handle to the large or small icon associated with a window. The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption. 
            /// </summary>
            GETICON = 0x007F,
            /// <summary>
            /// An application sends the WM_SETICON message to associate a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption. 
            /// </summary>
            SETICON = 0x0080,
            /// <summary>
            /// The WM_NCCREATE message is sent prior to the WM_CREATE message when a window is first created.
            /// </summary>
            NCCREATE = 0x0081,
            /// <summary>
            /// The WM_NCDESTROY message informs a window that its nonclient area is being destroyed. The DestroyWindow function sends the WM_NCDESTROY message to the window following the WM_DESTROY message. WM_DESTROY is used to free the allocated memory object associated with the window. 
            /// The WM_NCDESTROY message is sent after the child windows have been destroyed. In contrast, WM_DESTROY is sent before the child windows are destroyed.
            /// </summary>
            NCDESTROY = 0x0082,
            /// <summary>
            /// The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated. By processing this message, an application can control the content of the window's client area when the size or position of the window changes.
            /// </summary>
            NCCALCSIZE = 0x0083,
            /// <summary>
            /// The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released. If the mouse is not captured, the message is sent to the window beneath the cursor. Otherwise, the message is sent to the window that has captured the mouse.
            /// </summary>
            NCHITTEST = 0x0084,
            /// <summary>
            /// The WM_NCPAINT message is sent to a window when its frame must be painted. 
            /// </summary>
            NCPAINT = 0x0085,
            /// <summary>
            /// The WM_NCACTIVATE message is sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.
            /// </summary>
            NCACTIVATE = 0x0086,
            /// <summary>
            /// The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.
            /// </summary>
            GETDLGCODE = 0x0087,
            /// <summary>
            /// The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.
            /// </summary>
            SYNCPAINT = 0x0088,
            /// <summary>
            /// The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCMOUSEMOVE = 0x00A0,
            /// <summary>
            /// The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCLBUTTONDOWN = 0x00A1,
            /// <summary>
            /// The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCLBUTTONUP = 0x00A2,
            /// <summary>
            /// The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCLBUTTONDBLCLK = 0x00A3,
            /// <summary>
            /// The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCRBUTTONDOWN = 0x00A4,
            /// <summary>
            /// The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCRBUTTONUP = 0x00A5,
            /// <summary>
            /// The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCRBUTTONDBLCLK = 0x00A6,
            /// <summary>
            /// The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCMBUTTONDOWN = 0x00A7,
            /// <summary>
            /// The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCMBUTTONUP = 0x00A8,
            /// <summary>
            /// The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCMBUTTONDBLCLK = 0x00A9,
            /// <summary>
            /// The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCXBUTTONDOWN = 0x00AB,
            /// <summary>
            /// The WM_NCXBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCXBUTTONUP = 0x00AC,
            /// <summary>
            /// The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCXBUTTONDBLCLK = 0x00AD,
            /// <summary>
            /// The WM_INPUT_DEVICE_CHANGE message is sent to the window that registered to receive raw input. A window receives this message through its WindowProc function.
            /// </summary>
            INPUT_DEVICE_CHANGE = 0x00FE,
            /// <summary>
            /// The WM_INPUT message is sent to the window that is getting raw input. 
            /// </summary>
            INPUT = 0x00FF,
            /// <summary>
            /// This message filters for keyboard messages.
            /// </summary>
            KEYFIRST = 0x0100,
            /// <summary>
            /// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed. 
            /// </summary>
            KEYDOWN = 0x0100,
            /// <summary>
            /// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus. 
            /// </summary>
            KEYUP = 0x0101,
            /// <summary>
            /// The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed. 
            /// </summary>
            CHAR = 0x0102,
            /// <summary>
            /// The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character, such as the umlaut (double-dot), that is combined with another character to form a composite character. For example, the umlaut-O character (Ö) is generated by typing the dead key for the umlaut character, and then typing the O key. 
            /// </summary>
            DEADCHAR = 0x0103,
            /// <summary>
            /// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter. 
            /// </summary>
            SYSKEYDOWN = 0x0104,
            /// <summary>
            /// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter. 
            /// </summary>
            SYSKEYUP = 0x0105,
            /// <summary>
            /// The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is, a character key that is pressed while the ALT key is down. 
            /// </summary>
            SYSCHAR = 0x0106,
            /// <summary>
            /// The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is, a dead key that is pressed while holding down the ALT key. 
            /// </summary>
            SYSDEADCHAR = 0x0107,
            /// <summary>
            /// The WM_UNICHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_UNICHAR message contains the character code of the key that was pressed. 
            /// The WM_UNICHAR message is equivalent to WM_CHAR, but it uses Unicode Transformation Format (UTF)-32, whereas WM_CHAR uses UTF-16. It is designed to send or post Unicode characters to ANSI windows and it can can handle Unicode Supplementary Plane characters.
            /// </summary>
            UNICHAR = 0x0109,
            /// <summary>
            /// This message filters for keyboard messages.
            /// </summary>
            KEYLAST = 0x0109,
            /// <summary>
            /// Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_STARTCOMPOSITION = 0x010D,
            /// <summary>
            /// Sent to an application when the IME ends composition. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_ENDCOMPOSITION = 0x010E,
            /// <summary>
            /// Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_COMPOSITION = 0x010F,
            IME_KEYLAST = 0x010F,
            /// <summary>
            /// The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is displayed. Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box. 
            /// </summary>
            INITDIALOG = 0x0110,
            /// <summary>
            /// The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated. 
            /// </summary>
            COMMAND = 0x0111,
            /// <summary>
            /// A window receives this message when the user chooses a command from the Window menu, clicks the maximize button, minimize button, restore button, close button, or moves the form. You can stop the form from moving by filtering this out.
            /// </summary>
            SYSCOMMAND = 0x0112,
            /// <summary>
            /// The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function. 
            /// </summary>
            TIMER = 0x0113,
            /// <summary>
            /// The WM_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control. 
            /// </summary>
            HSCROLL = 0x0114,
            /// <summary>
            /// The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control. 
            /// </summary>
            VSCROLL = 0x0115,
            /// <summary>
            /// The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is displayed. 
            /// </summary>
            INITMENU = 0x0116,
            /// <summary>
            /// The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is displayed, without changing the entire menu. 
            /// </summary>
            INITMENUPOPUP = 0x0117,
            /// <summary>
            /// The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item. 
            /// </summary>
            MENUSELECT = 0x011F,
            /// <summary>
            /// The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu. 
            /// </summary>
            MENUCHAR = 0x0120,
            /// <summary>
            /// The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages. 
            /// </summary>
            ENTERIDLE = 0x0121,
            /// <summary>
            /// The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item. 
            /// </summary>
            MENURBUTTONUP = 0x0122,
            /// <summary>
            /// The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item. 
            /// </summary>
            MENUDRAG = 0x0123,
            /// <summary>
            /// The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item. 
            /// </summary>
            MENUGETOBJECT = 0x0124,
            /// <summary>
            /// The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed. 
            /// </summary>
            UNINITMENUPOPUP = 0x0125,
            /// <summary>
            /// The WM_MENUCOMMAND message is sent when the user makes a selection from a menu. 
            /// </summary>
            MENUCOMMAND = 0x0126,
            /// <summary>
            /// An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.
            /// </summary>
            CHANGEUISTATE = 0x0127,
            /// <summary>
            /// An application sends the WM_UPDATEUISTATE message to change the user interface (UI) state for the specified window and all its child windows.
            /// </summary>
            UPDATEUISTATE = 0x0128,
            /// <summary>
            /// An application sends the WM_QUERYUISTATE message to retrieve the user interface (UI) state for a window.
            /// </summary>
            QUERYUISTATE = 0x0129,
            /// <summary>
            /// The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message, the owner window can set the text and background colors of the message box by using the given display device context handle. 
            /// </summary>
            CTLCOLORMSGBOX = 0x0132,
            /// <summary>
            /// An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the edit control. 
            /// </summary>
            CTLCOLOREDIT = 0x0133,
            /// <summary>
            /// Sent to the parent window of a list box before the system draws the list box. By responding to this message, the parent window can set the text and background colors of the list box by using the specified display device context handle. 
            /// </summary>
            CTLCOLORLISTBOX = 0x0134,
            /// <summary>
            /// The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However, only owner-drawn buttons respond to the parent window processing this message. 
            /// </summary>
            CTLCOLORBTN = 0x0135,
            /// <summary>
            /// The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message, the dialog box can set its text and background colors using the specified display device context handle. 
            /// </summary>
            CTLCOLORDLG = 0x0136,
            /// <summary>
            /// The WM_CTLCOLORSCROLLBAR message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message, the parent window can use the display context handle to set the background color of the scroll bar control. 
            /// </summary>
            CTLCOLORSCROLLBAR = 0x0137,
            /// <summary>
            /// A static control, or an edit control that is read-only or disabled, sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the static control. 
            /// </summary>
            CTLCOLORSTATIC = 0x0138,
            /// <summary>
            /// Use WM_MOUSEFIRST to specify the first mouse message. Use the PeekMessage() Function.
            /// </summary>
            MOUSEFIRST = 0x0200,
            /// <summary>
            /// The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            MOUSEMOVE = 0x0200,
            /// <summary>
            /// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            LBUTTONDOWN = 0x0201,
            /// <summary>
            /// The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            LBUTTONUP = 0x0202,
            /// <summary>
            /// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            LBUTTONDBLCLK = 0x0203,
            /// <summary>
            /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            RBUTTONDOWN = 0x0204,
            /// <summary>
            /// The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            RBUTTONUP = 0x0205,
            /// <summary>
            /// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            RBUTTONDBLCLK = 0x0206,
            /// <summary>
            /// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            MBUTTONDOWN = 0x0207,
            /// <summary>
            /// The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            MBUTTONUP = 0x0208,
            /// <summary>
            /// The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            MBUTTONDBLCLK = 0x0209,
            /// <summary>
            /// The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
            /// </summary>
            MOUSEWHEEL = 0x020A,
            /// <summary>
            /// The WM_XBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse. 
            /// </summary>
            XBUTTONDOWN = 0x020B,
            /// <summary>
            /// The WM_XBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            XBUTTONUP = 0x020C,
            /// <summary>
            /// The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            XBUTTONDBLCLK = 0x020D,
            /// <summary>
            /// The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
            /// </summary>
            MOUSEHWHEEL = 0x020E,
            /// <summary>
            /// Use WM_MOUSELAST to specify the last mouse message. Used with PeekMessage() Function.
            /// </summary>
            MOUSELAST = 0x020E,
            /// <summary>
            /// The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed, or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created, the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns. When the child window is being destroyed, the system sends the message before any processing to destroy the window takes place.
            /// </summary>
            PARENTNOTIFY = 0x0210,
            /// <summary>
            /// The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered. 
            /// </summary>
            ENTERMENULOOP = 0x0211,
            /// <summary>
            /// The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited. 
            /// </summary>
            EXITMENULOOP = 0x0212,
            /// <summary>
            /// The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu. 
            /// </summary>
            NEXTMENU = 0x0213,
            /// <summary>
            /// The WM_SIZING message is sent to a window that the user is resizing. By processing this message, an application can monitor the size and position of the drag rectangle and, if needed, change its size or position. 
            /// </summary>
            SIZING = 0x0214,
            /// <summary>
            /// The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.
            /// </summary>
            CAPTURECHANGED = 0x0215,
            /// <summary>
            /// The WM_MOVING message is sent to a window that the user is moving. By processing this message, an application can monitor the position of the drag rectangle and, if needed, change its position.
            /// </summary>
            MOVING = 0x0216,
            /// <summary>
            /// Notifies applications that a power-management event has occurred.
            /// </summary>
            POWERBROADCAST = 0x0218,
            /// <summary>
            /// Notifies an application of a change to the hardware configuration of a device or the computer.
            /// </summary>
            DEVICECHANGE = 0x0219,
            /// <summary>
            /// An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window. 
            /// </summary>
            MDICREATE = 0x0220,
            /// <summary>
            /// An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window. 
            /// </summary>
            MDIDESTROY = 0x0221,
            /// <summary>
            /// An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window. 
            /// </summary>
            MDIACTIVATE = 0x0222,
            /// <summary>
            /// An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size. 
            /// </summary>
            MDIRESTORE = 0x0223,
            /// <summary>
            /// An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window. 
            /// </summary>
            MDINEXT = 0x0224,
            /// <summary>
            /// An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar, and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window. 
            /// </summary>
            MDIMAXIMIZE = 0x0225,
            /// <summary>
            /// An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format. 
            /// </summary>
            MDITILE = 0x0226,
            /// <summary>
            /// An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format. 
            /// </summary>
            MDICASCADE = 0x0227,
            /// <summary>
            /// An application sends the WM_MDIICONARRANGE message to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized. 
            /// </summary>
            MDIICONARRANGE = 0x0228,
            /// <summary>
            /// An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window. 
            /// </summary>
            MDIGETACTIVE = 0x0229,
            /// <summary>
            /// An application sends the WM_MDISETMENU message to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window, to replace the window menu of the frame window, or both. 
            /// </summary>
            MDISETMENU = 0x0230,
            /// <summary>
            /// The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns. 
            /// The system sends the WM_ENTERSIZEMOVE message regardless of whether the dragging of full windows is enabled.
            /// </summary>
            ENTERSIZEMOVE = 0x0231,
            /// <summary>
            /// The WM_EXITSIZEMOVE message is sent one time to a window, after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns. 
            /// </summary>
            EXITSIZEMOVE = 0x0232,
            /// <summary>
            /// Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.
            /// </summary>
            DROPFILES = 0x0233,
            /// <summary>
            /// An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window. 
            /// </summary>
            MDIREFRESHMENU = 0x0234,
            /// <summary>
            /// Sent to an application when a window is activated. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_SETCONTEXT = 0x0281,
            /// <summary>
            /// Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_NOTIFY = 0x0282,
            /// <summary>
            /// Sent by an application to direct the IME window to carry out the requested command. The application uses this message to control the IME window that it has created. To send this message, the application calls the SendMessage function with the following parameters.
            /// </summary>
            IME_CONTROL = 0x0283,
            /// <summary>
            /// Sent to an application when the IME window finds no space to extend the area for the composition window. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_COMPOSITIONFULL = 0x0284,
            /// <summary>
            /// Sent to an application when the operating system is about to change the current IME. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_SELECT = 0x0285,
            /// <summary>
            /// Sent to an application when the IME gets a character of the conversion result. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_CHAR = 0x0286,
            /// <summary>
            /// Sent to an application to provide commands and request information. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_REQUEST = 0x0288,
            /// <summary>
            /// Sent to an application by the IME to notify the application of a key press and to keep message order. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_KEYDOWN = 0x0290,
            /// <summary>
            /// Sent to an application by the IME to notify the application of a key release and to keep message order. A window receives this message through its WindowProc function. 
            /// </summary>
            IME_KEYUP = 0x0291,
            /// <summary>
            /// The WM_MOUSEHOVER message is posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to TrackMouseEvent.
            /// </summary>
            MOUSEHOVER = 0x02A1,
            /// <summary>
            /// The WM_MOUSELEAVE message is posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
            /// </summary>
            MOUSELEAVE = 0x02A3,
            /// <summary>
            /// The WM_NCMOUSEHOVER message is posted to a window when the cursor hovers over the nonclient area of the window for the period of time specified in a prior call to TrackMouseEvent.
            /// </summary>
            NCMOUSEHOVER = 0x02A0,
            /// <summary>
            /// The WM_NCMOUSELEAVE message is posted to a window when the cursor leaves the nonclient area of the window specified in a prior call to TrackMouseEvent.
            /// </summary>
            NCMOUSELEAVE = 0x02A2,
            /// <summary>
            /// The WM_WTSSESSION_CHANGE message notifies applications of changes in session state.
            /// </summary>
            WTSSESSION_CHANGE = 0x02B1,
            TABLET_FIRST = 0x02c0,
            TABLET_LAST = 0x02df,
            /// <summary>
            /// An application sends a WM_CUT message to an edit control or combo box to delete (cut) the current selection, if any, in the edit control and copy the deleted text to the clipboard in CF_TEXT format. 
            /// </summary>
            CUT = 0x0300,
            /// <summary>
            /// An application sends the WM_COPY message to an edit control or combo box to copy the current selection to the clipboard in CF_TEXT format. 
            /// </summary>
            COPY = 0x0301,
            /// <summary>
            /// An application sends a WM_PASTE message to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position. Data is inserted only if the clipboard contains data in CF_TEXT format. 
            /// </summary>
            PASTE = 0x0302,
            /// <summary>
            /// An application sends a WM_CLEAR message to an edit control or combo box to delete (clear) the current selection, if any, from the edit control. 
            /// </summary>
            CLEAR = 0x0303,
            /// <summary>
            /// An application sends a WM_UNDO message to an edit control to undo the last operation. When this message is sent to an edit control, the previously deleted text is restored or the previously added text is deleted.
            /// </summary>
            UNDO = 0x0304,
            /// <summary>
            /// The WM_RENDERFORMAT message is sent to the clipboard owner if it has delayed rendering a specific clipboard format and if an application has requested data in that format. The clipboard owner must render data in the specified format and place it on the clipboard by calling the SetClipboardData function. 
            /// </summary>
            RENDERFORMAT = 0x0305,
            /// <summary>
            /// The WM_RENDERALLFORMATS message is sent to the clipboard owner before it is destroyed, if the clipboard owner has delayed rendering one or more clipboard formats. For the content of the clipboard to remain available to other applications, the clipboard owner must render data in all the formats it is capable of generating, and place the data on the clipboard by calling the SetClipboardData function. 
            /// </summary>
            RENDERALLFORMATS = 0x0306,
            /// <summary>
            /// The WM_DESTROYCLIPBOARD message is sent to the clipboard owner when a call to the EmptyClipboard function empties the clipboard. 
            /// </summary>
            DESTROYCLIPBOARD = 0x0307,
            /// <summary>
            /// The WM_DRAWCLIPBOARD message is sent to the first window in the clipboard viewer chain when the content of the clipboard changes. This enables a clipboard viewer window to display the new content of the clipboard. 
            /// </summary>
            DRAWCLIPBOARD = 0x0308,
            /// <summary>
            /// The WM_PAINTCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area needs repainting. 
            /// </summary>
            PAINTCLIPBOARD = 0x0309,
            /// <summary>
            /// The WM_VSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's vertical scroll bar. The owner should scroll the clipboard image and update the scroll bar values. 
            /// </summary>
            VSCROLLCLIPBOARD = 0x030A,
            /// <summary>
            /// The WM_SIZECLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area has changed size. 
            /// </summary>
            SIZECLIPBOARD = 0x030B,
            /// <summary>
            /// The WM_ASKCBFORMATNAME message is sent to the clipboard owner by a clipboard viewer window to request the name of a CF_OWNERDISPLAY clipboard format.
            /// </summary>
            ASKCBFORMATNAME = 0x030C,
            /// <summary>
            /// The WM_CHANGECBCHAIN message is sent to the first window in the clipboard viewer chain when a window is being removed from the chain. 
            /// </summary>
            CHANGECBCHAIN = 0x030D,
            /// <summary>
            /// The WM_HSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window. This occurs when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's horizontal scroll bar. The owner should scroll the clipboard image and update the scroll bar values. 
            /// </summary>
            HSCROLLCLIPBOARD = 0x030E,
            /// <summary>
            /// This message informs a window that it is about to receive the keyboard focus, giving the window the opportunity to realize its logical palette when it receives the focus.
            /// </summary>
            QUERYNEWPALETTE = 0x030F,
            /// <summary>
            /// The WM_PALETTEISCHANGING message informs applications that an application is going to realize its logical palette. 
            /// </summary>
            PALETTEISCHANGING = 0x0310,
            /// <summary>
            /// This message is sent by the OS to all top-level and overlapped windows after the window with the keyboard focus realizes its logical palette. 
            /// This message enables windows that do not have the keyboard focus to realize their logical palettes and update their client areas.
            /// </summary>
            PALETTECHANGED = 0x0311,
            /// <summary>
            /// The WM_HOTKEY message is posted when the user presses a hot key registered by the RegisterHotKey function. The message is placed at the top of the message queue associated with the thread that registered the hot key. 
            /// </summary>
            HOTKEY = 0x0312,
            /// <summary>
            /// The WM_PRINT message is sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context.
            /// </summary>
            PRINT = 0x0317,
            /// <summary>
            /// The WM_PRINTCLIENT message is sent to a window to request that it draw its client area in the specified device context, most commonly in a printer device context.
            /// </summary>
            PRINTCLIENT = 0x0318,
            /// <summary>
            /// The WM_APPCOMMAND message notifies a window that the user generated an application command event, for example, by clicking an application command button using the mouse or typing an application command key on the keyboard.
            /// </summary>
            APPCOMMAND = 0x0319,
            /// <summary>
            /// The WM_THEMECHANGED message is broadcast to every window following a theme change event. Examples of theme change events are the activation of a theme, the deactivation of a theme, or a transition from one theme to another.
            /// </summary>
            THEMECHANGED = 0x031A,
            /// <summary>
            /// Sent when the contents of the clipboard have changed.
            /// </summary>
            CLIPBOARDUPDATE = 0x031D,
            /// <summary>
            /// The system will send a window the WM_DWMCOMPOSITIONCHANGED message to indicate that the availability of desktop composition has changed.
            /// </summary>
            DWMCOMPOSITIONCHANGED = 0x031E,
            /// <summary>
            /// WM_DWMNCRENDERINGCHANGED is called when the non-client area rendering status of a window has changed. Only windows that have set the flag DWM_BLURBEHIND.fTransitionOnMaximized to true will get this message. 
            /// </summary>
            DWMNCRENDERINGCHANGED = 0x031F,
            /// <summary>
            /// Sent to all top-level windows when the colorization color has changed. 
            /// </summary>
            DWMCOLORIZATIONCOLORCHANGED = 0x0320,
            /// <summary>
            /// WM_DWMWINDOWMAXIMIZEDCHANGE will let you know when a DWM composed window is maximized. You also have to register for this message as well. You'd have other windowd go opaque when this message is sent.
            /// </summary>
            DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
            /// <summary>
            /// Sent to request extended title bar information. A window receives this message through its WindowProc function.
            /// </summary>
            GETTITLEBARINFOEX = 0x033F,
            HANDHELDFIRST = 0x0358,
            HANDHELDLAST = 0x035F,
            AFXFIRST = 0x0360,
            AFXLAST = 0x037F,
            PENWINFIRST = 0x0380,
            PENWINLAST = 0x038F,
            /// <summary>
            /// The WM_APP constant is used by applications to help define private messages, usually of the form WM_APP+X, where X is an integer value. 
            /// </summary>
            APP = 0x8000,
            /// <summary>
            /// The WM_USER constant is used by applications to help define private messages for use by private window classes, usually of the form WM_USER+X, where X is an integer value. 
            /// </summary>
            USER = 0x0400,

            /// <summary>
            /// An application sends the WM_CPL_LAUNCH message to Windows Control Panel to request that a Control Panel application be started. 
            /// </summary>
            CPL_LAUNCH = USER + 0x1000,
            /// <summary>
            /// The WM_CPL_LAUNCHED message is sent when a Control Panel application, started by the WM_CPL_LAUNCH message, has closed. The WM_CPL_LAUNCHED message is sent to the window identified by the wParam parameter of the WM_CPL_LAUNCH message that started the application. 
            /// </summary>
            CPL_LAUNCHED = USER + 0x1001,
            /// <summary>
            /// WM_SYSTIMER is a well-known yet still undocumented message. Windows uses WM_SYSTIMER for internal actions like scrolling.
            /// </summary>
            SYSTIMER = 0x118
        }

        #region defines
        [Flags]
        public enum FreeType
        {
            Decommit = 0x4000,
            Release = 0x8000,
        }

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;   // size of the region allocated by the program
            public int State;   // check if allocated (MEM_COMMIT)
            public int Protect; // page protection (must be PAGE_READWRITE)
            public int lType;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        //[DllImport("user32.dll", SetLastError = true)]
        //static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
           uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          [Out] byte[] lpBuffer,
          int dwSize,
          out IntPtr lpNumberOfBytesRead
         );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
         IntPtr hProcess,
         IntPtr lpBaseAddress,
         [Out, MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
         int dwSize,
         out IntPtr lpNumberOfBytesRead
        );

        [DllImport("kernel32", SetLastError = true, PreserveSig = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess,
                                            IntPtr lpBaseAddress,
                                            [Out] byte[] lpBuffer,
                                            int dwSize,
                                            out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
         IntPtr hProcess,
         IntPtr lpBaseAddress,
         IntPtr lpBuffer,
         int dwSize,
         out IntPtr lpNumberOfBytesRead
        );

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;  // minimum address
            public IntPtr maximumApplicationAddress;  // maximum address
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;

            public static SYSTEM_INFO EMPTY => new SYSTEM_INFO();
        }

        public const int MONITORINFOF_PRIMARY = 1; // Primary display monitor flag

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumDelegate callback, int dwData);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFOEX
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szDevice;
        }

        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x16,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DisplayDevice
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        [DllImport("User32.dll")]
        public static extern int EnumDisplayDevices(string lpDevice, int iDevNum, ref DisplayDevice lpDisplayDevice, int dwFlags);

        //[StructLayout(LayoutKind.Sequential)]
        //public struct RECT
        //{
        //    public int Left;
        //    public int Top;
        //    public int Right;
        //    public int Bottom;
        //}

        public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, int dwData);

        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        // Some Win32 Struct in CSharp
        [StructLayout(LayoutKind.Sequential)]
        public struct LVITEM
        {
            public UInt32 mask;
            public Int32 iItem;
            public Int32 iSubItem;
            public UInt32 state;
            public UInt32 stateMask;
            public IntPtr pszText;
            public Int32 cchTextMax;
            public Int32 iImage;
            public IntPtr lParam;
            public Int32 iIndent;
            public Int32 iGroupId;
            public UInt32 cColumns;
            public UIntPtr puColumns;
            public IntPtr piColFmt;
            public Int32 iGroup;
        }

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        //TODO:
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern IntPtr GetModuleHandle(string lpModuleName);



        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool IsWow64Process([In] IntPtr Module, [Out] out bool WoW64Process);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        //TODO:
        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
           int dwSize, FreeType dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
         IntPtr hProcess,
         IntPtr lpBaseAddress,
        IntPtr lpBuffer,
         int dwSize,
         out int lpNumberOfBytesRead
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);
        #endregion

        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;
        private static IntPtr _hookID = IntPtr.Zero;

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData; // be careful, this must be ints, not uints (was wrong before I changed it...). regards, cmew.
            public int flags;
            public int time;
            public UIntPtr dwExtraInfo;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        public delegate IntPtr LowLevelKeyboardProc(
               int nCode, IntPtr wParam, IntPtr lParam);
        public static LowLevelKeyboardProc _proc = null;

        //[DllImport("user32.dll")]
        //public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam,
        //   IntPtr lParam);

        // overload for use with LowLevelKeyboardProc
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] KBDLLHOOKSTRUCT lParam);

        // overload for use with LowLevelMouseProc
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] MSLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            private int _Left;
            private int _Top;
            private int _Right;
            private int _Bottom;

            public RECT(RECT Rectangle)
                : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
            {
            }
            public RECT(int Left, int Top, int Right, int Bottom)
            {
                _Left = Left;
                _Top = Top;
                _Right = Right;
                _Bottom = Bottom;
            }

            public int X
            {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Y
            {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Left
            {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Top
            {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Right
            {
                get { return _Right; }
                set { _Right = value; }
            }
            public int Bottom
            {
                get { return _Bottom; }
                set { _Bottom = value; }
            }
            public int Height
            {
                get { return _Bottom - _Top; }
                set { _Bottom = value + _Top; }
            }
            public int Width
            {
                get { return _Right - _Left; }
                set { _Right = value + _Left; }
            }
            public Point Location
            {
                get { return new Point(Left, Top); }
                set
                {
                    _Left = value.X;
                    _Top = value.Y;
                }
            }
            public Size Size
            {
                get { return new Size(Width, Height); }
                set
                {
                    _Right = value.Width + _Left;
                    _Bottom = value.Height + _Top;
                }
            }

            public static implicit operator Rectangle(RECT Rectangle)
            {
                return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
            }
            public static implicit operator RECT(Rectangle Rectangle)
            {
                return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
            }
            public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
            {
                return Rectangle1.Equals(Rectangle2);
            }
            public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
            {
                return !Rectangle1.Equals(Rectangle2);
            }

            public override string ToString()
            {
                return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            public bool Equals(RECT Rectangle)
            {
                return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
            }

            public override bool Equals(object Object)
            {
                if (Object is RECT)
                {
                    return Equals((RECT)Object);
                }
                else if (Object is Rectangle)
                {
                    return Equals(new RECT((Rectangle)Object));
                }

                return false;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct GUITHREADINFO
        {
            public int cbSize;
            public uint flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public RECT rcCaret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idThread"></param>
        /// <param name="lpgui"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetGUIThreadInfo(uint idThread, out GUITHREADINFO lpgui);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nVirtKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern short GetKeyState(VirtualKeyStates nVirtKey);

        public enum VirtualKeyStates : int
        {
            VK_LBUTTON = 0x01,
            VK_RBUTTON = 0x02,
            VK_CANCEL = 0x03,
            VK_MBUTTON = 0x04,
            //
            VK_XBUTTON1 = 0x05,
            VK_XBUTTON2 = 0x06,
            //
            VK_BACK = 0x08,
            VK_TAB = 0x09,
            //
            VK_CLEAR = 0x0C,
            VK_RETURN = 0x0D,
            //
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_PAUSE = 0x13,
            VK_CAPITAL = 0x14,
            //
            VK_KANA = 0x15,
            VK_HANGEUL = 0x15,  /* old name - should be here for compatibility */
            VK_HANGUL = 0x15,
            VK_JUNJA = 0x17,
            VK_FINAL = 0x18,
            VK_HANJA = 0x19,
            VK_KANJI = 0x19,
            //
            VK_ESCAPE = 0x1B,
            //
            VK_CONVERT = 0x1C,
            VK_NONCONVERT = 0x1D,
            VK_ACCEPT = 0x1E,
            VK_MODECHANGE = 0x1F,
            //
            VK_SPACE = 0x20,
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_END = 0x23,
            VK_HOME = 0x24,
            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,
            VK_SELECT = 0x29,
            VK_PRINT = 0x2A,
            VK_EXECUTE = 0x2B,
            VK_SNAPSHOT = 0x2C,
            VK_INSERT = 0x2D,
            VK_DELETE = 0x2E,
            VK_HELP = 0x2F,
            //
            VK_LWIN = 0x5B,
            VK_RWIN = 0x5C,
            VK_APPS = 0x5D,
            //
            VK_SLEEP = 0x5F,
            //
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 0x64,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_MULTIPLY = 0x6A,
            VK_ADD = 0x6B,
            VK_SEPARATOR = 0x6C,
            VK_SUBTRACT = 0x6D,
            VK_DECIMAL = 0x6E,
            VK_DIVIDE = 0x6F,
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7A,
            VK_F12 = 0x7B,
            VK_F13 = 0x7C,
            VK_F14 = 0x7D,
            VK_F15 = 0x7E,
            VK_F16 = 0x7F,
            VK_F17 = 0x80,
            VK_F18 = 0x81,
            VK_F19 = 0x82,
            VK_F20 = 0x83,
            VK_F21 = 0x84,
            VK_F22 = 0x85,
            VK_F23 = 0x86,
            VK_F24 = 0x87,
            //
            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            //
            VK_OEM_NEC_EQUAL = 0x92,   // '=' key on numpad
            //
            VK_OEM_FJ_JISHO = 0x92,   // 'Dictionary' key
            VK_OEM_FJ_MASSHOU = 0x93,   // 'Unregister word' key
            VK_OEM_FJ_TOUROKU = 0x94,   // 'Register word' key
            VK_OEM_FJ_LOYA = 0x95,   // 'Left OYAYUBI' key
            VK_OEM_FJ_ROYA = 0x96,   // 'Right OYAYUBI' key
            //
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,
            VK_LCONTROL = 0xA2,
            VK_RCONTROL = 0xA3,
            VK_LMENU = 0xA4,
            VK_RMENU = 0xA5,
            //
            VK_BROWSER_BACK = 0xA6,
            VK_BROWSER_FORWARD = 0xA7,
            VK_BROWSER_REFRESH = 0xA8,
            VK_BROWSER_STOP = 0xA9,
            VK_BROWSER_SEARCH = 0xAA,
            VK_BROWSER_FAVORITES = 0xAB,
            VK_BROWSER_HOME = 0xAC,
            //
            VK_VOLUME_MUTE = 0xAD,
            VK_VOLUME_DOWN = 0xAE,
            VK_VOLUME_UP = 0xAF,
            VK_MEDIA_NEXT_TRACK = 0xB0,
            VK_MEDIA_PREV_TRACK = 0xB1,
            VK_MEDIA_STOP = 0xB2,
            VK_MEDIA_PLAY_PAUSE = 0xB3,
            VK_LAUNCH_MAIL = 0xB4,
            VK_LAUNCH_MEDIA_SELECT = 0xB5,
            VK_LAUNCH_APP1 = 0xB6,
            VK_LAUNCH_APP2 = 0xB7,
            //
            VK_OEM_1 = 0xBA,   // ';:' for US
            VK_OEM_PLUS = 0xBB,   // '+' any country
            VK_OEM_COMMA = 0xBC,   // ',' any country
            VK_OEM_MINUS = 0xBD,   // '-' any country
            VK_OEM_PERIOD = 0xBE,   // '.' any country
            VK_OEM_2 = 0xBF,   // '/?' for US
            VK_OEM_3 = 0xC0,   // '`~' for US
            //
            VK_OEM_4 = 0xDB,  //  '[{' for US
            VK_OEM_5 = 0xDC,  //  '\|' for US
            VK_OEM_6 = 0xDD,  //  ']}' for US
            VK_OEM_7 = 0xDE,  //  ''"' for US
            VK_OEM_8 = 0xDF,
            //
            VK_OEM_AX = 0xE1,  //  'AX' key on Japanese AX kbd
            VK_OEM_102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
            VK_ICO_HELP = 0xE3,  //  Help key on ICO
            VK_ICO_00 = 0xE4,  //  00 key on ICO
            //
            VK_PROCESSKEY = 0xE5,
            //
            VK_ICO_CLEAR = 0xE6,
            //
            VK_PACKET = 0xE7,
            //
            VK_OEM_RESET = 0xE9,
            VK_OEM_JUMP = 0xEA,
            VK_OEM_PA1 = 0xEB,
            VK_OEM_PA2 = 0xEC,
            VK_OEM_PA3 = 0xED,
            VK_OEM_WSCTRL = 0xEE,
            VK_OEM_CUSEL = 0xEF,
            VK_OEM_ATTN = 0xF0,
            VK_OEM_FINISH = 0xF1,
            VK_OEM_COPY = 0xF2,
            VK_OEM_AUTO = 0xF3,
            VK_OEM_ENLW = 0xF4,
            VK_OEM_BACKTAB = 0xF5,
            //
            VK_ATTN = 0xF6,
            VK_CRSEL = 0xF7,
            VK_EXSEL = 0xF8,
            VK_EREOF = 0xF9,
            VK_PLAY = 0xFA,
            VK_ZOOM = 0xFB,
            VK_NONAME = 0xFC,
            VK_PA1 = 0xFD,
            VK_OEM_CLEAR = 0xFE
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetWindowsDirectory(StringBuilder lpBuffer,
           uint uSize);

        public static string GetWindowsDir()
        {
            StringBuilder sDir = new StringBuilder(10, 100);
            GetWindowsDirectory(sDir, 500);
            return sDir.ToString();
        }

        /*public static string GetWindowsTempDir()
        {
            GetWindowsDir()
        }*/

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        ////Overload for string lParam (e.g. WM_GETTEXT)
        //[DllImport("user32.dll")]
        //public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [Out] StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern long SendMessage(IntPtr hWnd, int msg, int wParam, [Out] StringBuilder lParam);
        /*// Overloads use on toolbar/rebar
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref POINT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTON lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTONINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref REBARBANDINFO lParam);

        */
        public const int WM_GETTEXT = 0x0D;
        public const int WM_GETTEXTLENGTH = 0x000E;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetDlgItemText(IntPtr hDlg, int nIDDlgItem,
        [Out] StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern uint GetDlgItemInt(IntPtr hDlg, int nIDDlgItem, IntPtr
           lpTranslated, bool bSigned);

        [DllImport("User32.dll")]
        public static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        [DllImport("user32.dll")]
        public static extern bool CloseWindow(IntPtr hWnd);

        /// <summary>
        /// The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window's client area.
        /// </summary>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="X">Specifies the new position of the left side of the window.</param>
        /// <param name="Y">Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">Specifies the new width of the window.</param>
        /// <param name="nHeight">Specifies the new height of the window.</param>
        /// <param name="bRepaint">Specifies whether the window is to be repainted. If this parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of moving a child window.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, POINT Point);

        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hWndParent, Point pt, uint uFlags);

        /// <summary>
        /// <para>The DestroyWindow function destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY messages to the window to deactivate it and remove the keyboard focus from it. The function also destroys the window's menu, flushes the thread message queue, destroys timers, removes clipboard ownership, and breaks the clipboard viewer chain (if the window is at the top of the viewer chain).</para>
        /// <para>If the specified window is a parent or owner window, DestroyWindow automatically destroys the associated child or owned windows when it destroys the parent or owner window. The function first destroys child or owned windows, and then it destroys the parent or owner window.</para>
        /// <para>DestroyWindow also destroys modeless dialog boxes created by the CreateDialog function.</para>
        /// </summary>
        /// <param name="hwnd">Handle to the window to be destroyed.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        public enum GetAncestorFlags
        {
            /// <summary>
            /// Retrieves the parent window. This does not include the owner, as it does with the GetParent function. 
            /// </summary>
            GetParent = 1,
            /// <summary>
            /// Retrieves the root window by walking the chain of parent windows.
            /// </summary>
            GetRoot = 2,
            /// <summary>
            /// Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent. 
            /// </summary>
            GetRootOwner = 3
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowOwnedPopups(IntPtr hWnd, bool fShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Same as macro GetNextWindow() -> Z-order
        /// </summary>
        /// <param name="ptrHwnd"></param>
        /// <returns></returns>
        public static IntPtr GetNextWindow(IntPtr ptrHwnd)
        {
            return GetWindow(ptrHwnd, GW_HWNDNEXT);
        }

        public static int MakeWParam(int loWord, int hiWord)
        {
            return (loWord & 0xFFFF) + ((hiWord & 0xFFFF) << 16);
        }

        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            else
                return GetWindowLongPtr32(hWnd, nIndex);
        }

        /// <summary>
        /// Retrieves the handle to the ancestor of the specified window. 
        /// </summary>
        /// <param name="hwnd">A handle to the window whose ancestor is to be retrieved. 
        /// If this parameter is the desktop window, the function returns NULL. </param>
        /// <param name="flags">The ancestor to be retrieved.</param>
        /// <returns>The return value is the handle to the ancestor window.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags);


        /// <summary>
        /// Implemented and used by containers and objects to obtain window handles 
        /// and manage context-sensitive help.
        /// </summary>
        /// <remarks>
        /// The IOleWindow interface provides methods that allow an application to obtain 
        /// the handle to the various windows that participate in in-place activation, 
        /// and also to enter and exit context-sensitive help mode.
        /// </remarks>
        [ComImport]
        [Guid("00000114-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleWindow
        {
            /// <summary>
            /// Returns the window handle to one of the windows participating in in-place activation 
            /// (frame, document, parent, or in-place object window).
            /// </summary>
            /// <param name="phwnd">Pointer to where to return the window handle.</param>
            void GetWindow(out IntPtr phwnd);

            /// <summary>
            /// Determines whether context-sensitive help mode should be entered during an 
            /// in-place activation session.
            /// </summary>
            /// <param name="fEnterMode"><c>true</c> if help mode should be entered; 
            /// <c>false</c> if it should be exited.</param>
            void ContextSensitiveHelp([In, MarshalAs(UnmanagedType.Bool)] bool fEnterMode);
        }

        [Flags]
        public enum DeskBandInfoMasks
        {
            DBIM_MINSIZE = 0x0001,
            DBIM_MAXSIZE = 0x0002,
            DBIM_INTEGRAL = 0x0004,
            DBIM_ACTUAL = 0x0008,
            DBIM_TITLE = 0x0010,
            DBIM_MODEFLAGS = 0x0020,
            DBIM_BKCOLOR = 0x0040
        }

        [Flags]
        public enum DeskBandInfoModes : uint
        {
            /// <summary>
            /// The band is normal in all respects. The other mode flags modify this flag.
            /// </summary>
            Normal = 0x0000,
            /// <summary>
            /// TBD
            /// </summary>
            Fixed = 0x0001,
            /// <summary>
            /// a fixed background bitmap (if supported)
            /// </summary>
            FixedBMP = 0x0004,
            /// <summary>
            /// The height of the band object can be changed. 
            /// </summary>
            /// <remarks>
            /// The ptIntegral member defines the step value by which the band object can be resized. 
            /// </remarks>
            VariableHeight = 0x0008,
            /// <summary>
            /// TBD
            /// </summary>
            Undeleteable = 0x0010,
            /// <summary>
            /// The band object is displayed with a sunken appearance.
            /// </summary>
            Debossed = 0x0020,
            /// <summary>
            /// The band will be displayed with the background color specified in crBkgnd. 
            /// </summary>
            BackColor = 0x0040,
            /// <summary>
            /// Displays a chevron when the toolbar requires overflow.
            /// </summary>
            UseChevron = 0x0080,
            /// <summary>
            /// Display the toolbar in a new break
            /// </summary>
            Break = 0x0100,
            /// <summary>
            /// Adds the toolbar before the first toolbar in the frame
            /// </summary>
            AddToFront = 0x0200,
            /// <summary>
            /// Aligns the toolbar with the top of the frame
            /// </summary>
            TopAlign = 0x0400
        }

        [Flags]
        public enum DeskBandInfoViewMode : int
        {
            DBIF_VIEWMODE_NORMAL = 0x0000,
            DBIF_VIEWMODE_VERTICAL = 0x0001,
            DBIF_VIEWMODE_FLOATING = 0x0002,
            DBIF_VIEWMODE_TRANSPARENT = 0x0004
        }

        //[ComImport]
        //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        //[Guid("79D16DE4-ABEE-4021-8D9D-9169B261D657")]
        //public interface IDeskBand2
        //{
        //    // IOleWindow methods
        //    [PreserveSig]
        //    int GetWindow(out IntPtr phwnd);
        //    [PreserveSig]
        //    int ContextSensitiveHelp([In, MarshalAs(UnmanagedType.Bool)] bool fEnterMode);

        //    // IDockingWindow methods
        //    [PreserveSig]
        //    int ShowDW([In, MarshalAs(UnmanagedType.Bool)] bool fShow);
        //    [PreserveSig]
        //    int CloseDW([In] Int32 dwReserved);
        //    [PreserveSig]
        //    int ResizeBorderDW(ref RECT rcBorder, [In, MarshalAs(UnmanagedType.IUnknown)] ref object punkToolbarSite, [MarshalAs(UnmanagedType.Bool)] bool fReserved);

        //    // IDeskBand methods
        //    [PreserveSig]
        //    int GetBandInfo([In] Int32 dwBandID, [In] Int32 dwViewMode, [In, Out] ref DESKBANDINFO pdbi);

        //    // IDeskband2 methods
        //    void CanRenderComposited(out bool pfCanRenderComposited);
        //    void SetCompositionState(bool fCompositionEnabled);
        //    void GetCompositionState(out bool pfCompositionEnabled);
        //}


        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //public struct DESKBANDINFO
        //{
        //    /// <summary>
        //    /// Set of flags that determine which members of this structure are being requested. 
        //    /// </summary>
        //    /// <remarks>
        //    /// This will be a combination of the following values:
        //    ///     DBIM_MINSIZE    ptMinSize is being requested.
        //    ///     DBIM_MAXSIZE    ptMaxSize is being requested.
        //    ///     DBIM_INTEGRAL   ptIntegral is being requested.
        //    ///     DBIM_ACTUAL     ptActual is being requested.
        //    ///     DBIM_TITLE      wszTitle is being requested.
        //    ///     DBIM_MODEFLAGS  dwModeFlags is being requested.
        //    ///     DBIM_BKCOLOR    crBkgnd is being requested.
        //    /// </remarks>
        //    public DBIM dwMask;
        //    /// <summary>
        //    /// Point structure that receives the minimum size of the band object. 
        //    /// The minimum width is placed in the x member and the minimum height 
        //    /// is placed in the y member. 
        //    /// </summary>
        //    public Point ptMinSize;
        //    /// <summary>
        //    /// Point structure that receives the maximum size of the band object. 
        //    /// The maximum height is placed in the y member and the x member is ignored. 
        //    /// If there is no limit for the maximum height, (LONG)-1 should be used. 
        //    /// </summary>
        //    public Point ptMaxSize;
        //    /// <summary>
        //    /// Point structure that receives the sizing step value of the band object. 
        //    /// The vertical step value is placed in the y member, and the x member is ignored. 
        //    /// The step value determines in what increments the band will be resized. 
        //    /// </summary>
        //    /// <remarks>
        //    /// This member is ignored if dwModeFlags does not contain DBIMF_VARIABLEHEIGHT. 
        //    /// </remarks>
        //    public Point ptIntegral;
        //    /// <summary>
        //    /// Point structure that receives the ideal size of the band object. 
        //    /// The ideal width is placed in the x member and the ideal height is placed in the y member. 
        //    /// The band container will attempt to use these values, but the band is not guaranteed to be this size.
        //    /// </summary>
        //    public Point ptActual;
        //    /// <summary>
        //    /// The title of the band.
        //    /// </summary>
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        //    public String wszTitle;
        //    /// <summary>
        //    /// A value that receives a set of flags that define the mode of operation for the band object. 
        //    /// </summary>
        //    /// <remarks>
        //    /// This must be one or a combination of the following values.
        //    ///     DBIMF_NORMAL
        //    ///     The band is normal in all respects. The other mode flags modify this flag.
        //    ///     DBIMF_VARIABLEHEIGHT
        //    ///     The height of the band object can be changed. The ptIntegral member defines the 
        //    ///     step value by which the band object can be resized. 
        //    ///     DBIMF_DEBOSSED
        //    ///     The band object is displayed with a sunken appearance.
        //    ///     DBIMF_BKCOLOR
        //    ///     The band will be displayed with the background color specified in crBkgnd.
        //    /// </remarks>
        //    public DBIMF dwModeFlags;
        //    /// <summary>
        //    /// The background color of the band.
        //    /// </summary>
        //    /// <remarks>
        //    /// This member is ignored if dwModeFlags does not contain the DBIMF_BKCOLOR flag. 
        //    /// </remarks>
        //    public Int32 crBkgnd;
        //}

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        /// <summary>
        /// For use with ChildWindowFromPointEx
        /// </summary>
        [Flags]
        public enum WindowFromPointFlags
        {
            /// <summary>
            /// Does not skip any child windows
            /// </summary>
            CWP_ALL = 0x0000,
            /// <summary>
            /// Skips invisible child windows
            /// </summary>
            CWP_SKIPINVISIBLE = 0x0001,
            /// <summary>
            /// Skips disabled child windows
            /// </summary>
            CWP_SKIPDISABLED = 0x0002,
            /// <summary>
            /// Skips transparent child windows
            /// </summary>
            CWP_SKIPTRANSPARENT = 0x0004
        }

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hWndParent, POINT pt, uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr RealChildWindowFromPoint(IntPtr hwndParent, POINT ptParentClientCoords);

        [DllImport("user32.dll")]
        public static extern bool GetClassInfo(IntPtr hInstance, [Out] out string lpClassName,
           [Out] out WNDCLASS lpWndClass);

        public static int GetLastError()
        {
            return Marshal.GetLastWin32Error();
        }

        // from header files
        public const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
        public const uint FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
        public const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
        public const uint FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000;
        public const uint FORMAT_MESSAGE_FROM_HMODULE = 0x00000800;
        public const uint FORMAT_MESSAGE_FROM_STRING = 0x00000400;

        [DllImport("kernel32.dll")]
        public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource,
           uint dwMessageId, uint dwLanguageId, [Out] StringBuilder lpBuffer,
           uint nSize, IntPtr Arguments);

        // the version, the sample is built upon:
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource,
           uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer,
           uint nSize, IntPtr pArguments);

        // the parameters can also be passed as a string array:
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource,
           uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer,
           uint nSize, string[] Arguments);

        // see the sample code
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, [Out] StringBuilder lpBuffer, uint nSize, string[] Arguments);

        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASS
        {
            public ClassStyles style;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public WndProc lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszClassName;
        }

        public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [Flags]
        public enum ClassStyles : uint
        {
            /// <summary>Aligns the window's client area on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.</summary>
            ByteAlignClient = 0x1000,

            /// <summary>Aligns the window on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.</summary>
            ByteAlignWindow = 0x2000,

            /// <summary>
            /// Allocates one device context to be shared by all windows in the class.
            /// Because window classes are process specific, it is possible for multiple threads of an application to create a window of the same class.
            /// It is also possible for the threads to attempt to use the device context simultaneously. When this happens, the system allows only one thread to successfully finish its drawing operation.
            /// </summary>
            ClassDC = 0x40,

            /// <summary>Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is within a window belonging to the class.</summary>
            DoubleClicks = 0x8,

            /// <summary>
            /// Enables the drop shadow effect on a window. The effect is turned on and off through SPI_SETDROPSHADOW.
            /// Typically, this is enabled for small, short-lived windows such as menus to emphasize their Z order relationship to other windows.
            /// </summary>
            DropShadow = 0x20000,

            /// <summary>Indicates that the window class is an application global class. For more information, see the "Application Global Classes" section of About Window Classes.</summary>
            GlobalClass = 0x4000,

            /// <summary>Redraws the entire window if a movement or size adjustment changes the width of the client area.</summary>
            HorizontalRedraw = 0x2,

            /// <summary>Disables Close on the window menu.</summary>
            NoClose = 0x200,

            /// <summary>Allocates a unique device context for each window in the class.</summary>
            OwnDC = 0x20,

            /// <summary>
            /// Sets the clipping rectangle of the child window to that of the parent window so that the child can draw on the parent.
            /// A window with the CS_PARENTDC style bit receives a regular device context from the system's cache of device contexts.
            /// It does not give the child the parent's device context or device context settings. Specifying CS_PARENTDC enhances an application's performance.
            /// </summary>
            ParentDC = 0x80,

            /// <summary>
            /// Saves, as a bitmap, the portion of the screen image obscured by a window of this class.
            /// When the window is removed, the system uses the saved bitmap to restore the screen image, including other windows that were obscured.
            /// Therefore, the system does not send WM_PAINT messages to windows that were obscured if the memory used by the bitmap has not been discarded and if other screen actions have not invalidated the stored image.
            /// This style is useful for small windows (for example, menus or dialog boxes) that are displayed briefly and then removed before other screen activity takes place.
            /// This style increases the time required to display the window, because the system must first allocate memory to store the bitmap.
            /// </summary>
            SaveBits = 0x800,

            /// <summary>Redraws the entire window if a movement or size adjustment changes the height of the client area.</summary>
            VerticalRedraw = 0x1
        }


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(int hWnd, [Out] StringBuilder lpString,
        int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public class SearchData
        {
            // You can put any dicks or Doms in here...
            public string Wndclass;
            public string Title;
            public IntPtr hWnd;
        }

        public delegate bool EnumWindowsProcSearchData(IntPtr hWnd, ref SearchData data);
        public delegate int EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, ref SearchData data);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);

        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(POINT lpPoint);

        // P/Invoke für SDKs ShowWindow
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public const int SW_HIDE = 0;
        public const int SW_INVALIDATE = 0x02;
        public const int SW_MAX = 10;
        public const int SW_MAXIMIZE = 3;
        public const int SW_MINIMIZE = 6;
        public const int SW_NORMAL = 1;
        public const int SW_OTHERUNZOOM = 4;
        public const int SW_OTHERZOOM = 2;
        public const int SW_PARENTCLOSING = 1;
        public const int SW_PARENTOPENING = 3;
        public const int SW_RESTORE = 9;
        public const int SW_SCROLLCHILDREN = 0x01;
        public const int SW_SHOW = 5;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_SHOWNOACTIVATE = 4;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(Point _ptPoint)
                : this(_ptPoint.X, _ptPoint.Y)
            {
            }

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator POINT(Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool SetMenu(IntPtr hWnd, IntPtr hMenu);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMenuItemInfo(IntPtr hMenu, long un, bool b, ref MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        public static extern uint GetMenuItemID(IntPtr hMenu, int nPos);

        [DllImport("user32.dll")]
        public static extern bool SetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition,
           [In] ref MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem,
           uint uEnable);

        [DllImport("user32.dll")]
        public static extern bool CheckMenuRadioItem(IntPtr hmenu, uint idFirst, uint idLast,
           uint idCheck, uint uFlags);

        [DllImport("user32.dll")]
        public static extern int GetMenuString(IntPtr hMenu, uint uIDItem,
           [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder lpString, int nMaxCount, uint uFlag);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSubMenu(IntPtr hMenu, int nPos);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern long CreatePopupMenu();

        [DllImport("user32.dll")]
        public static extern long DestroyMenu(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool InsertMenuItem(IntPtr hMenu, uint uItem, bool fByPosition, [In] ref MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        public static extern bool ModifyMenu(IntPtr hMnu, uint uPosition, uint uFlags,
           UIntPtr uIDNewItem, string lpNewItem);

        [Flags]
        public enum MenuFlags : uint
        {
            MF_STRING = 0,
            MF_BYPOSITION = 0x400,
            MF_SEPARATOR = 0x800,
            MF_REMOVE = 0x1000,
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool AppendMenu(IntPtr hMenu, MenuFlags uFlags, uint uIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int wCmd);

        //[DllImport("gdi32.dll")]
        //public static extern bool GetWindowExtEx(IntPtr hdc, out SIZE lpSize);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern long GetWindowTextLength(IntPtr hWnd);

        /// <summary>The GetForegroundWindow function returns a handle to the foreground window.</summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsHungAppWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsGUIThread([MarshalAs(UnmanagedType.Bool)] bool bConvert);

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);

        public const int MDITILE_VERTICAL = 0;
        public const int MDITILE_HORIZONTAL = 1;

        [DllImport("user32.dll")]
        public static extern int TileWindows(IntPtr hwndParent, int wHow, IntPtr lpRect, int cKids, IntPtr lpKids);

        [DllImport("user32.dll", EntryPoint = "WindowFromPhysicalPoint", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern IntPtr IntWindowFromPhysicalPoint(POINTSTRUCT pt);

        public struct POINTSTRUCT
        {
            public int x;
            public int y;

            public POINTSTRUCT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static IntPtr WindowFromPhysicalPoint(int x, int y)
        {
            POINTSTRUCT ps = new POINTSTRUCT(x, y);
            if (System.Environment.OSVersion.Version.Major >= 6)
                return IntWindowFromPhysicalPoint(ps);
            else
                return IntWindowFromPoint(ps);
        }

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr IntWindowFromPoint(POINTSTRUCT pt);

        [DllImport("user32.dll")]
        public static extern uint RealGetWindowClass(IntPtr hwnd, [Out] StringBuilder pszType, uint cchType);

        public static string RealGetWindowClassM(IntPtr hWnd)
        {
            StringBuilder pszType = new StringBuilder();
            pszType.Capacity = 255;
            RealGetWindowClass(hWnd, pszType, (UInt32)pszType.Capacity);
            return pszType.ToString();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(int hWnd, int lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetOpenClipboardWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, [Out] StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, ref IntPtr lParam);

        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.Dll")]

        public static extern IntPtr EnumChildWindows(IntPtr parentHandle, Win32Callback callback, ref IntPtr lParam);
        // MENUITEMINFO fMask-Konstanten
        public const int MIIM_STATE = 0x01; // benutzt die fState-Optionen
        public const int MIIM_ID = 0x02; // benutzt die wID-Option
        public const int MIIM_SUBMENU = 0x04; // benutzt die hSubMenu-Option
        public const int MIIM_CHECKMARKS = 0x08; // benutzt die hbmpChecked- und hb,pUnchecked-Optionen 
        public const int MIIM_DATA = 0x020; // benutzt die dwItemDate-Option
        public const int MIIM_TYPE = 0x010; // benutzt die dwTypeData-Option

        public const int LVM_FIRST = 0x1000;
        public const int LVM_GETITEMCOUNT = LVM_FIRST + 4;
        public const int LVM_GETITEM = LVM_FIRST + 75;
        public const int LVIF_TEXT = 0x0001;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr window, int message, IntPtr wParam,
        IntPtr lParam);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, StringBuilder lParam);    

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr SendMessageByString(IntPtr hWnd, uint msg, int wParam, string lParam);

        [Flags]
        public enum LB
        {
            /* Multi-Select Only */
            /// <summary>
            ///   Selects an item in a multiple-selection list box and, if necessary, scrolls the item into view
            ///   wParam  Specifies how to set the selection. If this parameter is TRUE, the item is selected and highlighted; if it is FALSE, the highlight is removed and the item is no longer selected. 
            ///   lParam Specifies the zero-based index of the item to set. If this parameter is –1, the selection is added to or removed from all items, depending on the value of wParam, and no scrolling occurs. 
            ///   Return Value-If an error occurs, the return value is LB_ERR. 
            ///   Remarks-Use this message only with multiple-selection list boxes.
            /// </summary>
            LB_SETSEL = 0x0185,

            ///<summary>
            ///  Fills a buffer with an array of integers that specify the item numbers of selected items in a multiple-selection list box
            ///  wParam- The maximum number of selected items whose item numbers are to be placed in the buffer. 
            ///  lParam-A pointer to a buffer large enough for the number of integers specified by the wParam parameter. 
            ///  Return-The return value is the number of items placed in the buffer. If the list box is a single-selection list box, the return value is LB_ERR.
            ///</summary>
            LB_GETSELITEMS = 0x0191,

            /// <summary>
            ///   Gets the total number of selected items in a multiple-selection list box
            ///   wParam and lParam-Not used; must be zero. 
            ///   Return Value-The return value is the count of selected items in the list box. If the list box is a single-selection list box, the return value is LB_ERR.
            /// </summary>
            LB_GETSELCOUNT = 0x0190,

            /* Single Select Only */
            /// <summary>
            ///   Gets the index of the currently selected item, if any, in a single-selection list box
            ///   wParam and lParam Not used; must be zero. 
            ///   Return Value-In a single-selection list box, the return value is the zero-based index of the currently selected item. If there is no selection, the return value is LB_ERR.
            /// </summary>
            LB_GETCURSEL = 0x0188,

            /// <summary>
            ///   wParam The zero-based index of the item before the first item to be searched. When the search reaches the bottom of the list box, it continues from the top of the list box back to the item specified by the wParam parameter. If wParam is –1, the entire list box is searched from the beginning. 
            ///   lParam A pointer to the null-terminated string that contains the prefix for which to search. The search is case independent
            ///   Return Value If the search is successful, the return value is the index of the selected item. If the search is unsuccessful,LB_ERR
            ///   Use this message only with single-selection list boxes. You cannot use it to set or remove a selection in a multiple-selection list box
            /// </summary>
            LB_SELECTSTRING = 0x018C,

            /* Either */
            ///<summary>
            ///  Selects a string and scrolls it into view, if necessary. When the new string is selected, the list box removes the highlight from the previously selected string. 
            ///  wParam Specifies the zero-based index of the string that is selected. If this parameter is -1, the list box is set to have no selection. 
            ///  lParam not used
            ///</summary>
            LB_SETCURSEL = 0x0186,

            ///<summary>
            ///  Finds the first string in a list box that begins with the specified string
            ///  wParam The zero-based index of the item before the first item to be searched. When the search reaches the bottom of the list box, it continues searching from the top of the list box back to the item specified by the wParam parameter. If wParam is –1, the entire list box is searched from the beginning. 
            ///  lParam A pointer to the null-terminated string that contains the string for which to search. The search is case independent, so this string can contain any combination of uppercase and lowercase letters. 
            ///  Return Value-The return value is the index of the matching item, or LB_ERR if the search was unsuccessful.
            ///</summary>
            LB_FINDSTRING = 0x018F,

            /// <summary>
            ///   Gets the number of items in a list box
            ///   wParam and lParam-zero
            ///   The return value is the number of items in the list box, or LB_ERR if an error occurs
            /// </summary>
            LB_GETCOUNT = 0x018B,

            ///<summary>
            ///  Gets the selection state of an item
            ///  wParam The zero-based index of the item
            ///  lParam This parameter is not used
            ///  Return Value-If an item is selected, the return value is greater than zero; otherwise, it is zero. If an error occurs, the return value is LB_ERR.
            ///</summary>
            LB_GETSEL = 0x0187,

            ///<summary>
            ///  Gets a string from a list box. 
            ///  wParam The zero-based index of the string to retrieve. 
            ///  lParam A pointer to the buffer that will receive the string; The buffer must have sufficient space for the string and a terminating null character.
            ///  Return Value length of the string, in TCHARs, excluding the terminating null character.
            ///</summary>
            LB_GETTEXT = 0x0189,

            /// <summary>
            ///   Removes all items from a list box
            /// </summary>
            LB_RESETCONTENT = 0x0184,

            /// <summary>
            /// Sets the width, in pixels, by which a list box can be scrolled horizontally
            /// (the scrollable width). If the width of the list box is smaller than this
            /// value, the horizontal scroll bar horizontally scrolls items in the list box.
            /// If the width of the list box is equal to or greater than this value, the
            /// horizontal scroll bar is hidden.
            /// </summary>
            LB_SETHORIZONTALEXTENT = 0x0194,

            /// <summary>
            /// Gets the width, in pixels, that a list box can be scrolled horizontally
            /// (the scrollable width) if the list box has a horizontal scroll bar. 
            /// </summary>
            LB_GETHORIZONTALEXTENT = 0x0193,

            /// <summary>
            /// Gets the index of the first visible item in a list box. Initially the item
            /// with index 0 is at the top of the list box, but if the list box contents have
            /// been scrolled another item may be at the top. The first visible item in a
            /// multiple-column list box is the top-left item.
            /// </summary>
            LB_GETTOPINDEX = 0x018E,

            /// <summary>
            /// Ensures that the specified item in a list box is visible.
            /// </summary>
            LB_SETTOPINDEX = 0x0197,

            /// <summary>
            /// Inserts a string or item data into a list box. Unlike the LB_ADDSTRING
            /// message, the LB_INSERTSTRING message does not cause a list with the
            /// LBS_SORT style to be sorted.
            /// </summary>
            LB_INSERTSTRING = 0x0181,

            /// <summary>
            /// Deletes a string in a list box.
            /// </summary>
            LB_DELETESTRING = 0x0182,

            /// <summary>
            /// Gets the application-defined value associated with the specified list box item.
            /// </summary>
            LB_GETITEMDATA = 0x0199
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct LV_ITEM
        {
            public UInt32 mask;
            public Int32 iItem;
            public Int32 iSubItem;
            public UInt32 state;
            public UInt32 stateMask;
            public IntPtr pszText;
            public Int32 cchTextMax;
            public Int32 iImage;
            public IntPtr lParam;
        }

        // MENUITEMINFO fType-Konstanten
        public const int MFT_BITMAP = 0x04; // zeigt ein Bitmap im Menü an. Das Handle des Bitmaps  
        // muss in dwTypeData übergeben werden und cch wird ignoriert. Kann nicht mit  
        // MFT_SEPARATOR oder MFT_STRING kombiniert werden
        public const int MFT_MENUBARBREAK = 0x020; // platziert das Menü ein einer neuen Zeile oder  
        // Spalte und zeichnet über und unter dem Eintrag einen Separator
        public const int MFT_MENUBREAK = 0x040; // das Gleiche wie MFT_MENUBARBREAK nur ohne Separator 
        public const int MFT_OWNERDRAW = 0x0100; // überlässt das Neuzeichnen des Menüs dem Fenster
        public const int MFT_RADIOCHECK = 0x0200; // zeigt einen Radiobutton als Checked/Unchecked an 
        public const int MFT_RIGHTJUSTIFY = 0x04000; // richtet ein Menü rechtsbündig aus
        public const int MFT_RIGHTORDER = 0x02000; // (Win 9x, 2000) Die Menüs platzieren sich  
        // rechts voneinander und es wird Text von rechts nach links unterstützt
        public const int MFT_SEPARATOR = 0x0800; // zeichnet eine horizontale Linie in den  
        // Menüeintrag, dwTypeData und cch werden ignoriert. Kann nicht mit MFT_BITMAP oder  
        // MFT_STRING kombiniert werden
        public const int MFT_STRING = 0x00; // der Menüeintrag wird mit einem String gefüllt,  
        // deTypeData ist der String der angezeigt werden soll und cch die Länge des Strings. 
        // Kann nicht mit MFT_BITMAP oder MFT_SEPARATOR kombiniert werden

        // MENUITEMINFO fState-Konstanten
        public const int MFS_CHECKED = 0x08; // Menüeintrag ist markiert
        public const int MFS_DEFAULT = 0x01000; // Menüeintrag ist die Standard-Auswahl
        public const int MFS_DISABLED = 0x02; // Menüeintrag ist deaktiviert
        public const int MFS_ENABLED = 0x00; // Menüeintrag ist aktiviert
        public const int MFS_GRAYED = 0x01; // Menüeintrag ist grau und deaktiviert
        public const int MFS_HILITE = 0x080; // Menüeintrag hat die Selektierung
        public const int MFS_UNCHECKED = 0x00; // Menüeintrag ist nicht markiert
        public const int MFS_UNHILITE = 0x00; // Menüeintrag hat nicht die Selektierung

        // TrackPopupmenu uFlags-Konstanten
        public const int TPM_CENTERALIGN = 0x04; // positioniert das Menü horizontal in der Mitte  

        // von x
        public const int TPM_LEFTALIGN = 0x00; // positioniert das Menü horizontal mit dem linken  
        // Rand auf x
        public const int TPM_RIGHTALIGN = 0x08; // positioniert das Menü horizontal mit dem  
        // rechten Rand auf x
        public const int TPM_BOTTOMALIGN = 0x020; // positioniert das Menü mit dem unteren Rand  
        // auf y
        public const int TPM_TOPALIGN = 0x00;// positioniert das Menü mit dem oberen Rand auf y
        public const int TPM_VCENTERALIGN = 0x010; // positioniert das Menü vertikal in der Mitte  
        // von y
        public const int TPM_NONOTIFY = 0x080; // sendet kein WM_COMMAND an das Elternfenster des  
        // Menüs bei Ereignissen
        public const int TPM_RETURNCMD = 0x0100; // die Funktion gibt den ID des Menüs zurück  
        // das gewählt wurde
        public const int TPM_LEFTBUTTON = 0x00; // erlaubt dem Benutzer nur das Markieren der  
        // Einträge über die linke Maustaste und die Tastatur
        public const int TPM_RIGHTBUTTON = 0x02; // erlaubt dem Benutzer die Einträge mit jedem  
        // Mausbutton und der Tastatur zu wählen          */

        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
        public const int GW_MAX = 5;
        public const int GWW_HINSTANCE = (-6);
        public const int GWW_ID = (-12);
        public const int GWL_STYLE = (-16);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
           int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
           UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        //TODO:
        //Keys[] numberKeys = new Keys[10] { Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9 };

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi)]
        public static extern int mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, IntPtr hwndCallback);

        //TODO:
        //public static void PressKey(Keys key)
        //{
        //    const int KEYEVENTF_EXTENDEDKEY = 0x1;
        //    const int KEYEVENTF_KEYUP = 0x2;
        //    // I had some Compile errors until I Casted the final 0 to UIntPtr like this...
        //    keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
        //    keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
        //}

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Beep(uint dwFreq, uint dwDuration);

        [DllImport("user32.dll")]
        public static extern short VkKeyScan(char ch);

        //TODO:
        //public static void PressKeyArray(Keys[] keys)
        //{
        //    foreach (Keys key in keys)
        //    {
        //        PressKey(key);
        //    }
        //}

        public const int WH_MOUSE_LL = 14;

        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202
        }

        public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);


        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        //    IntPtr wParam, IntPtr lParam);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfInputs"></param>
        /// <param name="input"></param>
        /// <param name="structSize"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] input, int structSize);

        /// <summary>
        /// The GetMessageExtraINfo function retrieves the extra message informationmfor the current thread. 
        /// Extra Message Information is an application- or driver-defined value associated with the current thread's message queue.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)] //*
            public MOUSEINPUT mi;
            [FieldOffset(4)] //*
            public KEYBDINPUT ki;
            [FieldOffset(4)] //*
            public HARDWAREINPUT hi;
        }
    }
}
