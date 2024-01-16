Public Class ErrorHandlers
    Public errorHandlersLabel As New Label
    Private Sub ErrorHandlers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 408, 88)
        InitializeComponent()
    End Sub
    Sub New()
        InitializeComponent()
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateErrorHandlers()
    End Sub
    Private Sub CreateErrorHandlers()
        errorHandlersLabel = Tools.CreateErrorHandlers(Me, 323, 30, 47, 28)
        errorHandlersLabel.ForeColor = Color.Black
    End Sub
End Class
