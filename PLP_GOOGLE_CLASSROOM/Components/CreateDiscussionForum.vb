Imports System.IO

Public Class CreateDiscussionForum
    Public tableName = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Dim dicussionTitle As New TextBox
    Dim discussionDescription As New TextBox
    Dim attachFile As New TextBox
    Dim attachFilePath As String = String.Empty
    Private Sub CreateDiscussionForum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 676, 777)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        AttachFileButton()
        CloseButton()
        DiscussionTitleTextBox()
        DiscussionDescriptionTextBox()
        PostDiscussionButton()
    End Sub
    Private Sub CloseButton()
        Dim closeBtn As Label = Tools.CreateButton(Me, 42, 24, 594, 28)
        AddHandler closeBtn.Click, Sub(sender As Object, e As EventArgs)
                                       TeacherDashboard.createDiscussionForumPanelPopup.Visible = False
                                   End Sub
    End Sub
    Private Sub DiscussionTitleTextBox()
        dicussionTitle = Tools.CreateTextBox(Me, "Discussion title", 550, 50, 60, 180)
    End Sub
    Private Sub DiscussionDescriptionTextBox()
        discussionDescription = Tools.CreateTextBox(Me, "Discussion Description", 550, 170, 60, 284)
        discussionDescription.Multiline = True
    End Sub
    Private Sub AttachFileButton()
        attachFile = Tools.CreateTextBox(Me, "Find a file", 500, 50, 60, 532)
        attachFile.ReadOnly = True
        AddHandler attachFile.Click, Sub(sender As Object, e As EventArgs)
                                         OpenFileExplorer()
                                     End Sub
    End Sub
    Private Sub OpenFileExplorer()
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.InitialDirectory = "C:\"
        openFileDialog.Filter = "PDF Files|*.pdf|All Files|*.*"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim selectedFilePath As String = openFileDialog.FileName

            Dim fileName As String = Path.GetFileName(selectedFilePath)

            attachFile.Text = fileName

            attachFilePath = selectedFilePath
        End If
    End Sub
    Private Sub PostDiscussionButton()
        Dim button As Label = Tools.CreateButton(Me, 591, 95, 39, 648)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.CreateClassDiscussion(tableName, dicussionTitle.Text, discussionDescription.Text, attachFile.Text, attachFilePath, tableName)
                                     TeacherDashboard.createDiscussionForumPanelPopup.Visible = False
                                 End Sub
    End Sub
End Class
