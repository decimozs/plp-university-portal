Public Class StudentAnnouncementDashboard
    Dim announcementPanel As New FlowLayoutPanel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentAnnouncementDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        HideScrollBar()
        CreateAnnouncementClass()
        RefreshButton()
    End Sub
    Public Sub CreateAnnouncementClass()
        Dim announcements As List(Of Announcement) = FetchAllAnnouncements()

        Dim panelWidth As Integer = 1019
        Dim panelHeight As Integer = 369
        Dim spaceBetweenPanels As Integer = 10

        announcementPanel.BackColor = Color.Transparent
        announcementPanel.Size = New Size(1389, 654)
        announcementPanel.Location = New Point(0, 62)
        announcementPanel.FlowDirection = FlowDirection.TopDown
        announcementPanel.WrapContents = False
        announcementPanel.AutoScroll = True

        If announcements.Count > 0 Then
            For Each announcement In announcements
                Dim xCoordinate As Integer = 0
                Dim yCoordinate As Integer = If(announcementPanel.Controls.Count > 0, announcementPanel.Controls(announcementPanel.Controls.Count - 1).Bottom + spaceBetweenPanels, 0)

                Dim announcementClassPanel As Panel = Tools.CreatePanel(announcementPanel, panelWidth, panelHeight, xCoordinate, yCoordinate)
                Handlers.PopulateAnnouncementPanel(announcementClassPanel, announcements, announcements.IndexOf(announcement))
                announcementPanel.Controls.Add(announcementClassPanel)
            Next
        Else
            NoClassAvailable()
        End If

        Me.Controls.Add(announcementPanel)
    End Sub
    Private Sub NoClassAvailable()
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = "There are currently no announcements."
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub HideScrollBar()
        Dim label As Label = Tools.CreateUserLabel(Me, 50, 742, 1350, 0)
        label.BackColor = Color.White
    End Sub
    Private Sub RefreshButton()
        Dim button As Label = Tools.CreateUserLabel(Me, 66, 66, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.RefreshPanel(announcementPanel)
                                     CreateAnnouncementClass()
                                 End Sub
    End Sub
End Class
