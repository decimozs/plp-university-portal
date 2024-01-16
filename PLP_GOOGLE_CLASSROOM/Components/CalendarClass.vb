Public Class CalendarClass
    Private Sub CalendarClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 489, 310)
    End Sub
    Sub New(currentDate As String, task As String)
        InitializeComponent()
        CreateCalendarClass(currentDate, task)
    End Sub
    Private Sub CreateCalendarClass(currentDate As String, task As String)
        Dim currentDateLbl As Label = Tools.CreateUserLabel(Me, 400, 71, 41, 50)
        currentDateLbl.Text = currentDate
        currentDateLbl.Font = New Font("Poppins", 18, FontStyle.Bold)

        Dim taskLbl As Label = Tools.CreateUserLabel(Me, 300, 71, 41, 220)
        taskLbl.Text = task
        taskLbl.Font = New Font("Poppins", 22, FontStyle.Bold)
        taskLbl.ForeColor = ColorTranslator.FromHtml("#959595")
    End Sub
End Class
