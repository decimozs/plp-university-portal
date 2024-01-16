Public Class StudentDiscussionForumDashboard
    Public parseId = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Dim discussionPanel As New FlowLayoutPanel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentDiscussionForumDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        HideScrollBar()
        CreateDiscussionPanel()
        RefreshButton()
    End Sub
    Public Sub CreateDiscussionPanel()
        Dim discussions As List(Of DiscussionForum) = FethAllDiscussionForum("2200648")

        Dim panelWidth As Integer = 1019
        Dim panelHeight As Integer = 369
        Dim spaceBetweenPanels As Integer = 10

        discussionPanel.BackColor = Color.Transparent
        discussionPanel.Size = New Size(1389, 654)
        discussionPanel.Location = New Point(0, 62)
        discussionPanel.FlowDirection = FlowDirection.TopDown
        discussionPanel.WrapContents = False
        discussionPanel.AutoScroll = True

        If discussions.Count > 0 Then
            For Each discussion In discussions
                Dim xCoordinate As Integer = 0
                Dim yCoordinate As Integer = If(discussionPanel.Controls.Count > 0, discussionPanel.Controls(discussionPanel.Controls.Count - 1).Bottom + spaceBetweenPanels, 0)

                Dim announcementClassPanel As Panel = Tools.CreatePanel(discussionPanel, panelWidth, panelHeight, xCoordinate, yCoordinate)
                Handlers.PopulateDiscussionPanel(announcementClassPanel, discussions, discussions.IndexOf(discussion))
                discussionPanel.Controls.Add(announcementClassPanel)
            Next
        Else
            NoDiscussionAvailable()
        End If

        Me.Controls.Add(discussionPanel)
    End Sub
    Private Sub NoDiscussionAvailable()
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = "There are currently no discussions you posted."
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
                                     Handlers.RefreshPanel(discussionPanel)
                                     CreateDiscussionPanel()
                                 End Sub
    End Sub
End Class
