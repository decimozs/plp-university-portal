Imports System.Security.Cryptography
Imports System.Text

Module Handlers
    Public Sub TestButton()
        MsgBox("Button is working!")
    End Sub
    Public Sub CloseWindow(component As Form)
        component.Hide()
        component.Close()
        component.Dispose()
        Application.Exit()
    End Sub
    Public Sub DisposeWindow(currentForm As Form, newForm As Form)
        currentForm.Hide()
        currentForm.Dispose()
        newForm.Show()
    End Sub
    Public Sub MinimizedWindow(component As Form)
        component.WindowState = FormWindowState.Minimized
    End Sub
    Public Sub OpenWindow(currentForm As Form, newForm As Form)
        currentForm.Hide()
        newForm.Show()
    End Sub
    Public Sub OpenPanel(component As Panel)
        component.Visible = Not component.Visible
        component.BringToFront()
    End Sub
    Public Function IdParser(id As String) As String
        Dim result As String = id.Replace("-", "")
        Return result
    End Function
    Public Function GenerateUniqueClassNumber() As String
        Dim allowedCharacters As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim length As Integer = 6
        Dim randomBytes(length - 1) As Byte

        Using rngCryptoServiceProvider As New RNGCryptoServiceProvider()
            rngCryptoServiceProvider.GetBytes(randomBytes)
        End Using

        Dim result As New StringBuilder(length)

        For Each randomByte As Byte In randomBytes
            Dim index As Integer = randomByte Mod allowedCharacters.Length
            result.Append(allowedCharacters(index))
        Next

        Return result.ToString()
    End Function
    Public Function FilePath(filename As String) As String
        Return $"C:\Users\acer\Documents\get yo ass up\get yo ass up on vb\PLP_GOOGLE_CLASSROOM\PLP_GOOGLE_CLASSROOM\Resources\{filename}.png"
    End Function
    Public Sub StudentLoginAccess(username As String, password As String)
        If Database.StudentAuthorizationAccess(username, password) Then
            Handlers.OpenWindow(StudentLoginForm, StudentDashboard)
        Else
            StudentLoginForm.errorHandlersPanel.Visible = True
            StudentLoginForm.ErrorHandlers()
        End If
    End Sub
    Public Sub TeacherLoginAccess(username As String, password As String)
        If Database.TeacherAuthorizationAccess(username, password) Then
            Handlers.OpenWindow(TeacherLoginForm, TeacherDashboard)
        Else
            TeacherLoginForm.errorHandlersPanel.Visible = True
            TeacherLoginForm.ErrorHandlers()
        End If
    End Sub
    Public Function YearLevel(year As String) As String
        Select Case year
            Case "1st Year"
                Return $"{year} - Freshmen"
            Case "2nd Year"
                Return $"{year} - Sophomore"
            Case "3rd Year"
                Return $"{year} - Junior"
            Case "4th Year"
                Return $"{year} - Senior"
            Case Else
                Return "Unknown Year"
        End Select
    End Function
    Public Sub RefreshPanel(panel As FlowLayoutPanel)
        panel.Controls.Clear()
        panel.Invalidate()
        panel.Update()
    End Sub
    Public Sub PopulateClassPanel(panel As Panel, courses As List(Of [Class]), index As Integer)
        If index < courses.Count Then
            Dim course As [Class] = courses(index)
            Dim teacherClassInstance As New TeacherClass(course.CourseID, course.CourseProgram, course.CourseName, course.CourseDescription, "overview", course.ClassCode)
            panel.Controls.Add(teacherClassInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateAnnouncementPanel(panel As Panel, announcements As List(Of Announcement), index As Integer)
        If index < announcements.Count Then
            Dim announcement As Announcement = announcements(index)
            Dim announcementClassInstance As New AnnouncementClass(announcement.AnnouncementID, announcement.AnnouncementTitle, announcement.Description, announcement.TeacherNo)
            panel.Controls.Add(announcementClassInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateDiscussionPanel(panel As Panel, discussions As List(Of DiscussionForum), index As Integer)
        If index < discussions.Count Then
            Dim discussion As DiscussionForum = discussions(index)
            Dim discussionClassInstance As New DiscussionForumClass(discussion.DiscussionID, discussion.DiscussionTitle, discussion.Description, discussion.FileName, discussion.FilePath, discussion.TeacherNo)
            panel.Controls.Add(discussionClassInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateStudentClassCourse(panel As Panel, courses As List(Of StudentClassCourse), index As Integer)
        If index < courses.Count Then
            Dim course As StudentClassCourse = courses(index)
            Dim studentCourseInstance As New StudentCourse(course.CourseID, course.CourseProgram, course.CourseName, course.CourseTeacher, course.CourseUnits, course.ClassCode, course.CourseDescription)
            panel.Controls.Add(studentCourseInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateAvailableAnnouncement(panel As Panel, announcements As List(Of Announcement), index As Integer)
        If index < announcements.Count Then
            Dim announcement As Announcement = announcements(index)
            Dim announcementClassInstance As New AnnouncementOverviewComponent(announcement.AnnouncementID, announcement.AnnouncementTitle, announcement.Description, announcement.TeacherNo)
            panel.Controls.Add(announcementClassInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateTodo(panel As Panel, todos As List(Of Todo), index As Integer)
        If index < todos.Count Then
            Dim todo As Todo = todos(index)
            Dim todoClassInstance As New TodoClass(todo.TodoID, todo.Title, todo.Description, todo.Filename, todo.Filepath, todo.CreatedAt, todo.Deadline, todo.TeacherNo)
            panel.Controls.Add(todoClassInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateAvailableTodo(panel As Panel, todos As List(Of Todo), index As Integer)
        If index < todos.Count Then
            Dim todo As Todo = todos(index)
            Dim todoClassInstance As New TodoOverviewComponent(todo.TodoID, todo.Title, todo.Description, todo.Filename, todo.Filepath, todo.CreatedAt, todo.Deadline, todo.Grade, todo.TeacherNo)
            panel.Controls.Add(todoClassInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulatePassedTodo(panel As Panel, todos As List(Of Todo), index As Integer)
        If index < todos.Count Then
            Dim todo As Todo = todos(index)
            Dim todoClassInstance As New StudentPassedTodo(todo.TodoID, todo.Title, todo.StudentName, todo.Description, todo.CreatedAt, todo.Filename, todo.Filepath, todo.Deadline, todo.Grade, todo.TeacherNo)
            panel.Controls.Add(todoClassInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateListTodo(panel As Panel, todos As List(Of Todo), index As Integer)
        If index < todos.Count Then
            Dim todo As Todo = todos(index)
            Dim todoClassInstance As New ListTodoClass(todo.TodoID, todo.Title, todo.Description, todo.Filename, todo.Filepath, todo.CreatedAt, todo.Deadline, todo.TeacherNo, todo.StudentName, todo.Grade)
            panel.Controls.Add(todoClassInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateCalendar(panel As Panel, assignedTasks As List(Of Calendar), index As Integer)
        If index < assignedTasks.Count Then
            Dim assignedTask As Calendar = assignedTasks(index)
            Dim assignTaskInstance As New CalendarClass(assignedTask.CurrentDate, assignedTask.Task)
            panel.Controls.Add(assignTaskInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub PopulateStudentTeachers(panel As Panel, teachers As List(Of Teacher), index As Integer)
        If index < teachers.Count Then
            Dim teacher As Teacher = teachers(index)
            Dim teacherInstance As New StudentTeacherClass(teacher.TeacherNo, teacher.CourseTeacher, teacher.TeacherRole, teacher.CourseProgram, teacher.CourseName)
            panel.Controls.Add(teacherInstance)
            panel.Visible = True
        End If
    End Sub
    Public Sub CreateClassCourse(tableName As String, classCode As String, courseProgram As String, courseName As String, courseDescription As String, courseUnits As String, teacherName As String, teacherId As String)
        Database.AddCourse(tableName, classCode, courseProgram, courseName, courseDescription, courseUnits, teacherName, teacherId)
    End Sub
    Public Sub CreateClassAnnouncement(announcementTitle As String, description As String, teacherId As String)
        Database.AddAnnouncement(announcementTitle, description, teacherId)
    End Sub
    Public Sub CreateClassDiscussion(tableName As String, discussionTitle As String, description As String, fileName As String, filePath As String, teacherNo As Integer)
        Database.AddDiscussion(tableName, discussionTitle, description, fileName, filePath, teacherNo)
    End Sub
    Public Sub CreateClassTodo(tableName As String, title As String, description As String, fileName As String, filePath As String, createdAt As String, deadline As String, grade As String, teacherNo As Integer)
        Database.AddTeacherTodo(tableName, title, description, fileName, filePath, createdAt, deadline, grade, teacherNo)
        Database.AddStudentTodo(tableName, title, description, fileName, filePath, createdAt, deadline, grade, teacherNo)
    End Sub
    Public Sub CreateStudentTodo(tableName As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As Integer)
        Database.AddStudentTodo(tableName, title, description, filename, filepath, createdAt, deadline, grade, teacherNo)
    End Sub
    Public Sub DeleteClassCourse(tableName As String, courseId As String)
        Database.DeleteClass(tableName, courseId)
    End Sub
    Public Sub DeleteStudentClassCourse(tableName As String, courseId As String)
        Database.DeleteStudentClass(tableName, courseId)
        ' test 
        Database.DeleteTodoClass()
    End Sub
    Public Sub UpdateTodoGrade(username As String, id As String, newGrade As String, teacherTable As String)
        Database.UpdateGrade(username, id, newGrade)
        Database.UpdateTeacherGrade(teacherTable, id, newGrade)
    End Sub
    Public Sub CreateTeacherResources(teacherId As String)
        Database.CreateTeacherResources(teacherId)
        Database.CreateAnnouncement()
        Database.CreateDiscussionForum(teacherId)
        Database.CreateTeacherTodoResources(teacherId)
    End Sub
    Public Sub CreateStudentResources(studentId As String)
        Database.CreateStudentResources(studentId)
        Database.CreateStudentTodoResources(studentId)
        Database.CreateStudentPassedTodos(studentId)
    End Sub
    Public Sub SubmitStudentTodo(username As String, todoID As Integer, studentName As String, title As String, createdAt As String, grade As String, filename As String, filepath As String, teacherNo As Integer)
        Database.SubmitTodo(username, todoID, studentName, title, createdAt, grade, filename, filepath, teacherNo)
    End Sub
    Public Function GetStudentFirstName(studentId As String) As String
        Return Database.FetchStudentFirstName(studentId)
    End Function
    Public Function GetStudentSection(studentId As String) As String
        Return Database.FetchStudentSection(studentId)
    End Function
    Public Function GetStudentName(studentId As String) As String
        Return Database.FetchStudentName(studentId)
    End Function
    Public Function GetStudentYear(studentId As String) As String
        Return Database.FetchStudentYear(studentId)
    End Function
    Public Function GetStudentSemester(studentId As String) As String
        Return Database.FetchStudentSemester(studentId)
    End Function
    Public Function GetTeacherName(teacherId As String) As String
        Return Database.FetchTeacherName(teacherId)
    End Function
    Public Function GetTeacherPosition(teacherId As String) As String
        Return Database.FetchTeacherPosition(teacherId)
    End Function
    Public Function GetTeacherDepartment(teacherId As String) As String
        Return Database.FetchTeacherDeparment(teacherId)
    End Function
    Public Function GetTeacherFirstName(teacherId As String) As String
        Return Database.FetchTeacherFirstName(teacherId)
    End Function
End Module
