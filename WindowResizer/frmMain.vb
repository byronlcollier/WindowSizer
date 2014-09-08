Imports System.Runtime.InteropServices
Public Class frmWindowResizer
    Dim lstResolutionList As New List(Of Resolution)
    <DllImport("user32.dll", EntryPoint:="MoveWindow", CharSet:=CharSet.Auto)> _
    Public Shared Function MoveWindow(ByVal hWnd As IntPtr, _
        ByVal X As Int32, _
        ByVal Y As Int32, _
        ByVal nWidth As Int32, _
        ByVal nHeight As Int32, _
        ByVal bRepaint As Boolean _
        ) As Boolean
    End Function
    Private Sub btnResize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResize.Click
        Dim oItem As runningProcess = CType(cbDropDown.SelectedItem, runningProcess)
        Dim intX As Integer
        Dim intY As Integer
        If Not cbDropDown.SelectedItem Is Nothing Then
            intX = Convert.ToInt32(txtWidth.Text)
            intY = Convert.ToInt32(txtHeight.Text)
            MoveWindow(oItem.Value, 10, 10, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(txtHeight.Text), True) 'Resize
        End If
    End Sub
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call Initialise()
    End Sub
    Private Sub Initialise()
        Call GetProcessList()
        Call InitialiseResolutionList()
        Call LoadResolutionComboBox()
    End Sub
    Private Sub InitialiseResolutionList()
        lstResolutionList.Add(New Resolution("SVGA", "4:3", 800, 600))
        lstResolutionList.Add(New Resolution("WSVGA", "~17:10", 1024, 600))
        lstResolutionList.Add(New Resolution("XGA", "4:3", 1024, 768))
        lstResolutionList.Add(New Resolution("XGA+", "4:3", 1152, 864))
        lstResolutionList.Add(New Resolution("WXGA", "16:9", 1280, 720))
        lstResolutionList.Add(New Resolution("WXGA", "5:3", 1280, 768))
        lstResolutionList.Add(New Resolution("WXGA", "16:10", 1280, 800))
        lstResolutionList.Add(New Resolution("SXGA– (UVGA)", "4:3", 1280, 960))
        lstResolutionList.Add(New Resolution("SXGA", "5:4", 1280, 1024))
        lstResolutionList.Add(New Resolution("HD", "~16:9", 1360, 768))
        lstResolutionList.Add(New Resolution("HD", "~16:9", 1366, 768))
        lstResolutionList.Add(New Resolution("SXGA+", "4:3", 1400, 1050))
        lstResolutionList.Add(New Resolution("WXGA+", "16:10", 1440, 900))
        lstResolutionList.Add(New Resolution("HD+", "16:9", 1600, 900))
        lstResolutionList.Add(New Resolution("UXGA", "4:3", 1600, 1200))
        lstResolutionList.Add(New Resolution("WSXGA+", "16:10", 1680, 1050))
        lstResolutionList.Add(New Resolution("FHD", "16:9", 1920, 1080))
        lstResolutionList.Add(New Resolution("WUXGA", "16:10", 1920, 1200))
        lstResolutionList.Add(New Resolution("QWXGA", "16:9", 2048, 1152))
        lstResolutionList.Add(New Resolution("WQHD", "16:9", 2560, 1440))
        lstResolutionList.Add(New Resolution("WQXGA", "16:10", 2560, 1600))
    End Sub
    Private Sub LoadResolutionComboBox()
        For Each res As Resolution In lstResolutionList
            Me.cboPreSetResolutions.Items.Add(res.Name & " (" & res.AspectRatio & ")")
        Next
    End Sub
    Private Sub cbRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRefresh.Click
        Call GetProcessList()
    End Sub
    Public Sub GetProcessList()
        Dim procs() As Process = Process.GetProcesses()
        Dim proc As Process
        Dim hWnd As IntPtr
        For Each proc In procs
            hWnd = proc.MainWindowHandle
            If Not (hWnd = IntPtr.Zero) Then
                cbDropDown.Items.Add(New runningProcess(proc.MainWindowTitle, proc.MainWindowHandle))
            End If
        Next proc
    End Sub
    Private Sub cboPreSetResolutions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPreSetResolutions.SelectedIndexChanged
        Dim i As Integer
        i = cboPreSetResolutions.SelectedIndex
        txtWidth.Text = lstResolutionList(i).Width
        txtHeight.Text = lstResolutionList(i).Height
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub
End Class
Public Class runningProcess
    Private mText As String
    Private mValue As String

    Public Sub New(ByVal pText As String, ByVal pValue As String)
        mText = pText
        mValue = pValue
    End Sub

    Public ReadOnly Property Text() As String
        Get
            Return mText
        End Get
    End Property

    Public ReadOnly Property Value() As String
        Get
            Return mValue
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return mText
    End Function
End Class
Public Class Resolution
    Private strAbbreviation As String
    Private strAspectRatio As String
    Private intWidth As Integer
    Private intHeight As Integer
    Public Sub New(ByVal Name As String, ByVal AspectRatio As String, ByVal Width As Integer, ByVal Height As Integer)
        strAbbreviation = Name
        strAspectRatio = AspectRatio
        intWidth = Width
        intHeight = Height
    End Sub
    Public Property Name As String
        Get
            Return strAbbreviation
        End Get
        Set(ByVal value As String)
            strAbbreviation = value
        End Set
    End Property
    Public Property AspectRatio As String
        Get
            Return strAspectRatio
        End Get
        Set(ByVal value As String)
            strAspectRatio = value
        End Set
    End Property
    Public Property Width As Integer
        Get
            Return intWidth
        End Get
        Set(ByVal value As Integer)
            intWidth = value
        End Set
    End Property
    Public Property Height As Integer
        Get
            Return intHeight
        End Get
        Set(ByVal value As Integer)
            intHeight = value
        End Set
    End Property
End Class
