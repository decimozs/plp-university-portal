Imports System.Diagnostics.Eventing
Imports System.Windows.Forms.VisualStyles
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Module Database
    Private ReadOnly server As String = "localhost"
    Private ReadOnly database As String = "plp_university_portal_db"
    Private ReadOnly user As String = "root"
    Private ReadOnly password As String = "admin"

    Private ReadOnly token As String = $"Server={server};Database={database};User={user};Password={password};"

    Public Function GetConnection() As MySqlConnection
        Return New MySqlConnection(token)
    End Function
    Public Function GetCommand(query As String) As MySqlCommand
        Dim connection As MySqlConnection = GetConnection()
        connection.Open()
        Return New MySqlCommand(query, connection)
    End Function
    Public Sub TestDatabaseConnection()
        Using connection As MySqlConnection = GetConnection()
            Try
                connection.Open()
                MsgBox("Connection successful!")
            Catch ex As Exception
                MsgBox($"Connection failed: {ex.Message}")
            End Try
        End Using
    End Sub
    Public Function StudentAuthorizationAccess(username As String, password As String) As Boolean
        Dim query As String = $"select Username, Password from StudentAuthorization where Username = '{username}' and Password = '{password}'"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Dim reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    reader.Close()
                    Return True
                Else
                    reader.Close()
                    Return False
                End If
            End Using
        End Using
    End Function
    Public Function TeacherAuthorizationAccess(username As String, password As String) As Boolean
        Dim query As String = $"select Username, Password from TeacherAuthorization where Username = '{username}' and Password = '{password}'"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Dim reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    reader.Close()
                    CreateTeacherResources(username)
                    Return True
                Else
                    reader.Close()
                    Return False
                End If
            End Using
        End Using
    End Function
    Public Function CreateTeacherResources(username As String) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"CREATE TABLE IF NOT EXISTS {usernameParse}_Teacher_Courses (" &
            "CourseID INTEGER PRIMARY KEY AUTO_INCREMENT," &
            "ClassCode TEXT," &
            "CourseProgram TEXT," &
            "CourseName TEXT," &
            "CourseDescription TEXT," &
            "CourseUnits TEXT," &
            "TeacherName TEXT," &
            "TeacherNo INTEGER," &
            "FOREIGN KEY (TeacherNo) REFERENCES Teachers(TeacherNo));"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    MsgBox($"Error: {ex}")
                    Return False
                End Try
            End Using
        End Using
    End Function
    Public Function CreateStudentResources(username As String) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"CREATE TABLE IF NOT EXISTS {usernameParse}_Student_Courses (" &
            "CourseID INTEGER PRIMARY KEY AUTO_INCREMENT," &
            "ClassCode TEXT," &
            "CourseProgram TEXT," &
            "CourseName TEXT," &
            "CourseDescription TEXT," &
            "CourseUnits TEXT," &
            "TeacherName TEXT," &
            "TeacherNo INTEGER," &
            "FOREIGN KEY (TeacherNo) REFERENCES Teachers(TeacherNo));"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    MsgBox($"Error: {ex}")
                    Return False
                End Try
            End Using
        End Using
    End Function
    Public Function CreateStudentPassedTodos(username As String) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"CREATE TABLE IF NOT EXISTS {usernameParse}_Student_Passed_Todos (" &
        "TodoID INTEGER PRIMARY KEY AUTO_INCREMENT," &
        "StudentName TEXT," &
        "Title TEXT," &
        "CreatedAt TEXT," &
        "Grade TEXT," &
        "Filename TEXT," &
        "Filepath TEXT," &
        "TeacherNo INTEGER," &
        "FOREIGN KEY (TeacherNo) REFERENCES Teachers(TeacherNo));"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    MsgBox($"Error: {ex}")
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Function SubmitTodo(username As String, todoID As Integer, studentName As String, title As String, createdAt As String, grade As String, filename As String, filepath As String, teacherNo As Integer) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim tableName As String = $"{usernameParse}_Student_Passed_Todos"
        Dim query As String = $"INSERT INTO {tableName} (TodoID, StudentName, Title, CreatedAt, Grade, Filename, Filepath, TeacherNo) " &
                           $"VALUES (@TodoID, @StudentName, @Title, @CreatedAt, @Grade, @Filename, @Filepath, @TeacherNo);"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.Parameters.AddWithValue("@TodoID", todoID)
                    command.Parameters.AddWithValue("@StudentName", studentName)
                    command.Parameters.AddWithValue("@Title", title)
                    command.Parameters.AddWithValue("@CreatedAt", createdAt)
                    command.Parameters.AddWithValue("@Grade", grade)
                    command.Parameters.AddWithValue("@Filename", filename)
                    command.Parameters.AddWithValue("@Filepath", filepath)
                    command.Parameters.AddWithValue("@TeacherNo", teacherNo)

                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    MsgBox($"Error: {ex}")
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Function CreateAnnouncement() As Boolean
        Dim query As String = $"CREATE TABLE IF NOT EXISTS Annoucements (" &
            "AnnouncementID INTEGER PRIMARY KEY AUTO_INCREMENT," &
            "AnnouncmentTitle TEXT," &
            "Description TEXT," &
            "TeacherNo INTEGER," &
            "FOREIGN KEY (TeacherNo) REFERENCES Teachers(TeacherNo));"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    MsgBox($"Error: {ex}")
                    Return False
                End Try
            End Using
        End Using
    End Function
    Public Function InsertClassDetails(usernameParse As String, classCode As String) As Boolean
        Dim querySelect As String = $"SELECT * FROM 2200648_Teacher_Courses WHERE ClassCode = @ClassCode;"
        Dim queryInsert As String = $"INSERT INTO {usernameParse}_Student_Courses (ClassCode, CourseProgram, CourseName, CourseDescription, CourseUnits, TeacherName, TeacherNo) VALUES (@ClassCode, @CourseProgram, @CourseName, @CourseDescription, @CourseUnits, @TeacherName, @TeacherNo);"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()

            Using selectCommand As MySqlCommand = GetCommand(querySelect)
                selectCommand.Parameters.AddWithValue("@ClassCode", classCode)

                Using reader As MySqlDataReader = selectCommand.ExecuteReader()
                    If reader.Read() Then
                        Dim courseProgram As String = reader.GetString("CourseProgram")
                        Dim courseName As String = reader.GetString("CourseName")
                        Dim courseDescription As String = reader.GetString("CourseDescription")
                        Dim courseUnits As String = reader.GetString("CourseUnits")
                        Dim teacherName As String = reader.GetString("TeacherName")
                        Dim teacherNo As Integer = reader.GetInt32("TeacherNo")

                        Using insertCommand As MySqlCommand = GetCommand(queryInsert)
                            insertCommand.Parameters.AddWithValue("@ClassCode", classCode)
                            insertCommand.Parameters.AddWithValue("@CourseProgram", courseProgram)
                            insertCommand.Parameters.AddWithValue("@CourseName", courseName)
                            insertCommand.Parameters.AddWithValue("@CourseDescription", courseDescription)
                            insertCommand.Parameters.AddWithValue("@CourseUnits", courseUnits)
                            insertCommand.Parameters.AddWithValue("@TeacherName", teacherName)
                            insertCommand.Parameters.AddWithValue("@TeacherNo", teacherNo)

                            insertCommand.ExecuteNonQuery()

                            Dim update As New StudentOverviewDashboard
                            update.availableCoursesPanel.Controls.Clear()
                            update.availableCoursesPanel.Invalidate()
                            update.availableCoursesPanel.Update()
                            StudentDashboard.CoursesDashboardPanel()

                            Return True
                        End Using
                    Else
                        MsgBox("Class not found.", MsgBoxStyle.Exclamation, "Error")
                        Return False
                    End If
                End Using
            End Using
        End Using
    End Function
    Public Function FetchStudentTeacher(username As String) As List(Of Teacher)
        Dim usernameParse = Handlers.IdParser(username)
        Dim teachers As New List(Of Teacher)
        Dim query As String = $"select CourseID, CourseProgram, CourseName, TeacherName, TeacherNo from {username}_Student_Courses"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader
                    Try
                        While reader.Read
                            Dim teacher As New Teacher() With {
                                .CourseID = reader("CourseID").ToString(),
                                .CourseProgram = reader("CourseProgram").ToString(),
                                .CourseName = reader("CourseName").ToString(),
                                .CourseTeacher = reader("TeacherName").ToString(),
                                .TeacherNo = reader("TeacherNo").ToString()
                            }
                            teachers.Add(teacher)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using
        Return teachers
    End Function
    Public Function FetchAssignTask(username As String) As List(Of Calendar)
        Dim assignTaskes As New List(Of Calendar)
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"select Title, Deadline from {usernameParse}_Teacher_Todo"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader
                    Try
                        While reader.Read
                            Dim task As New Calendar() With {
                                .CurrentDate = reader("Deadline").ToString(),
                                .Task = reader("Title").ToString()
                            }
                            assignTaskes.Add(task)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using
        Return assignTaskes
    End Function
    Public Function FetchAllAnnouncements() As List(Of Announcement)
        Dim announcements As New List(Of Announcement)
        Dim query As String = $"SELECT * FROM Annoucements;"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()

            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    Try
                        While reader.Read()
                            Dim announcement As New Announcement() With {
                            .AnnouncementID = reader("AnnouncementID").ToString(),
                            .AnnouncementTitle = reader("AnnouncmentTitle").ToString(),
                            .Description = reader("Description").ToString(),
                            .TeacherNo = reader("TeacherNo").ToString()
                        }
                            announcements.Add(announcement)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using

        Return announcements
    End Function
    Public Function FetchAllTodo(username As String) As List(Of Todo)
        Dim todos As New List(Of Todo)
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"select * from {usernameParse}_Teacher_Todo"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader
                    Try
                        While reader.Read
                            Dim todo As New Todo() With {
                                .TodoID = reader("TodoID").ToString(),
                                .Title = reader("Title").ToString(),
                                .Description = reader("Description").ToString(),
                                .Filename = reader("Filename").ToString(),
                                .Filepath = reader("Filepath").ToString(),
                                .CreatedAt = reader("CreatedAt").ToString(),
                                .Deadline = reader("Deadline").ToString(),
                                .TeacherNo = reader("TeacherNO").ToString()
                            }
                            todos.Add(todo)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using

        Return todos
    End Function
    Public Function FetchPassedTodo(username As String) As List(Of Todo)
        Dim todos As New List(Of Todo)
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"select * from {usernameParse}_Student_Passed_Todos"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader
                    Try
                        While reader.Read
                            Dim todo As New Todo() With {
                                .TodoID = reader("TodoID").ToString(),
                                .StudentName = reader("StudentName").ToString(),
                                .Title = reader("Title").ToString(),
                                .CreatedAt = reader("CreatedAt").ToString(),
                                .Grade = reader("Grade").ToString(),
                                .Filename = reader("Filename").ToString(),
                                .Filepath = reader("Filepath").ToString(),
                                .TeacherNo = reader("TeacherNO").ToString()
                            }
                            todos.Add(todo)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using

        Return todos
    End Function
    Public Function FethAllDiscussionForum(username As String) As List(Of DiscussionForum)
        Dim discussions As New List(Of DiscussionForum)
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"SELECT * FROM {usernameParse}_Teacher_DiscussionForum;"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()

            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    Try
                        While reader.Read()
                            Dim discussion As New DiscussionForum() With {
                            .DiscussionID = reader("DiscussionForumID").ToString(),
                            .DiscussionTitle = reader("DiscussionTitle").ToString(),
                            .Description = reader("DiscussionDescription").ToString(),
                            .FileName = reader("Filename").ToString(),
                            .FilePath = reader("Filepath").ToString(),
                            .TeacherNo = reader("TeacherNo").ToString()
                        }
                            discussions.Add(discussion)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using

        Return discussions
    End Function
    Public Function FetchStudentSection(studentId As String) As String
        Dim studentSection As String = String.Empty
        Dim query As String = $"SELECT Section FROM Students WHERE StudentNo = {studentId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim sectionIndex As Integer = reader.GetOrdinal("Section")

                        If Not reader.IsDBNull(sectionIndex) Then
                            Dim section As String = reader.GetString(sectionIndex)
                            studentSection = section
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return studentSection
    End Function
    Public Function CreateDiscussionForum(username As String) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"CREATE TABLE IF NOT EXISTS {usernameParse}_Teacher_DiscussionForum (" &
            "DiscussionForumID INTEGER PRIMARY KEY AUTO_INCREMENT," &
            "DiscussionTitle TEXT," &
            "DiscussionDescription TEXT," &
            "Filename TEXT," &
            "Filepath TEXT," &
            "TeacherNo INTEGER," &
            "FOREIGN KEY (TeacherNo) REFERENCES Teachers(TeacherNo));"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    MsgBox($"Error: {ex}")
                    Return False
                End Try
            End Using
        End Using
    End Function
    'test only
    Public Function CreateTeacherTodoResources(username As String) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"CREATE TABLE IF NOT EXISTS {usernameParse}_Teacher_Todo (" &
            "TodoID INTEGER PRIMARY KEY AUTO_INCREMENT," &
            "Title TEXT," &
            "Description TEXT," &
            "Filename TEXT," &
            "Filepath TEXT," &
            "CreatedAt TEXT," &
            "Deadline TEXT," &
            "GRADE TEXT," &
            "TeacherNO INT," &
            "FOREIGN KEY (TeacherNo) REFERENCES Teachers(TeacherNo));"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    MsgBox($"Error: {ex}")
                    Return False
                End Try
            End Using
        End Using
    End Function
    Public Function CreateStudentTodoResources(username As String) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"CREATE TABLE IF NOT EXISTS {usernameParse}_Student_Todo (" &
            "TodoID INTEGER PRIMARY KEY AUTO_INCREMENT," &
            "Title TEXT," &
            "Description TEXT," &
            "Filename TEXT," &
            "Filepath TEXT," &
            "CreatedAt TEXT," &
            "Deadline TEXT," &
            "Grade TEXT," &
            "TeacherNO INT," &
            "FOREIGN KEY (TeacherNo) REFERENCES Teachers(TeacherNo));"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    MsgBox($"Error: {ex}")
                    Return False
                End Try
            End Using
        End Using
    End Function
    Public Function AddAnnouncement(announcementTitle As String, description As String, teacherNo As Integer) As Boolean
        Try
            Dim query As String = "INSERT INTO Annoucements (AnnouncmentTitle, Description, TeacherNo) VALUES (@AnnouncementTitle, @Description, @TeacherNo);"

            Using connection As MySqlConnection = GetConnection()
                connection.Open()

                Using command As MySqlCommand = GetCommand(query)
                    command.Parameters.AddWithValue("@AnnouncementTitle", announcementTitle)
                    command.Parameters.AddWithValue("@Description", description)
                    command.Parameters.AddWithValue("@TeacherNo", teacherNo)

                    command.ExecuteNonQuery()
                End Using
            End Using

            Dim update As New AnnouncementDashboard
            update.announcementPanel.Controls.Clear()
            update.announcementPanel.Invalidate()
            update.announcementPanel.Update()
            TeacherDashboard.AnnouncementDashboardPanel()

            Return True
        Catch ex As Exception
            MsgBox($"Error: {ex}")
            Return False
        End Try
    End Function

    Public Function AddCourse(tableName As String, classCode As String, courseProgram As String, courseName As String, courseDescription As String, units As String, teacherName As String, teacherNo As Integer) As Boolean
        Try
            Dim query As String = $"INSERT INTO {tableName}_Teacher_Courses (ClassCode, CourseProgram, CourseName, CourseDescription, CourseUnits, TeacherName, TeacherNo) " &
                              $"VALUES (@ClassCode, @CourseProgram, @CourseName, @CourseDescription, @CourseUnits, @TeacherName, @TeacherNo);"

            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Using command As MySqlCommand = GetCommand(query)
                    command.Parameters.AddWithValue("@ClassCode", classCode)
                    command.Parameters.AddWithValue("@CourseProgram", courseProgram)
                    command.Parameters.AddWithValue("@CourseName", courseName)
                    command.Parameters.AddWithValue("@CourseDescription", courseDescription)
                    command.Parameters.AddWithValue("@CourseUnits", units)
                    command.Parameters.AddWithValue("@TeacherName", teacherName)
                    command.Parameters.AddWithValue("@TeacherNo", teacherNo)

                    command.ExecuteNonQuery()
                End Using
            End Using

            Dim form As New TeacherClassDashboard
            form.availableClassesPanel.Controls.Clear()
            form.availableClassesPanel.Invalidate()
            form.availableClassesPanel.Update()
            TeacherDashboard.ClassDashboardPanel()

            Return True
        Catch ex As Exception
            MsgBox($"Error: {ex}")
            Return False
        End Try
    End Function

    Public Function AddDiscussion(tableName As String, discussionTitle As String, description As String, fileName As String, filePath As String, teacherNo As Integer) As Boolean
        Try
            Dim query As String = $"INSERT INTO {tableName}_Teacher_DiscussionForum (DiscussionTitle, DiscussionDescription, Filename, Filepath, TeacherNo) " &
                          $"VALUES (@DiscussionTitle, @DiscussionDescription, @Filename, @Filepath, @TeacherNo);"

            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Using command As MySqlCommand = GetCommand(query)
                    command.Parameters.AddWithValue("@DiscussionTitle", discussionTitle)
                    command.Parameters.AddWithValue("@DiscussionDescription", description)
                    command.Parameters.AddWithValue("@Filename", fileName)
                    command.Parameters.AddWithValue("@Filepath", filePath.Replace("/", "\"))
                    command.Parameters.AddWithValue("@TeacherNo", teacherNo)

                    command.ExecuteNonQuery()

                End Using
            End Using

            Dim update As New DiscussionForumDashboard
            update.discussionPanel.Controls.Clear()
            update.discussionPanel.Invalidate()
            update.discussionPanel.Update()
            TeacherDashboard.DiscussionForumDashboardPanel()

            Return True
        Catch ex As Exception
            MsgBox($"Error: {ex}")
            Return False
        End Try
    End Function

    Public Function AddTeacherTodo(tableName As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As Integer) As Boolean
        Dim query As String = $"insert into {tableName}_Teacher_Todo (Title, Description, Filename, Filepath, CreatedAt, Deadline, TeacherNO) " &
                               $"values (@Title, @Description, @Filename, @Filepath, @CreatedAt, @Deadline, @TeacherNO);"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.Parameters.AddWithValue("@Title", title)
                    command.Parameters.AddWithValue("@Description", description)
                    command.Parameters.AddWithValue("@Filename", filename)
                    command.Parameters.AddWithValue("@Filepath", filepath)
                    command.Parameters.AddWithValue("@CreatedAt", createdAt)
                    command.Parameters.AddWithValue("Deadline", deadline)
                    command.Parameters.AddWithValue("Grade", grade)
                    command.Parameters.AddWithValue("TeacherNO", teacherNo)

                    command.ExecuteNonQuery()
                Catch ex As Exception
                    Return False
                End Try
            End Using
        End Using

        ' test onlyv
        Dim form As New ViewClassCourse("", "", "", "", "")
        form.todosPanel.Controls.Clear()
        form.todosPanel.Invalidate()
        form.todosPanel.Update()

        Return True
    End Function
    Public Function AddStudentTodo(tableName As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As Integer) As Boolean
        Dim query As String = $"insert into {tableName}_Student_Todo (Title, Description, Filename, Filepath, CreatedAt, Deadline, TeacherNO) " &
                               $"values (@Title, @Description, @Filename, @Filepath, @CreatedAt, @Deadline, @TeacherNO);"
        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    command.Parameters.AddWithValue("@Title", title)
                    command.Parameters.AddWithValue("@Description", description)
                    command.Parameters.AddWithValue("@Filename", filename)
                    command.Parameters.AddWithValue("@Filepath", filepath)
                    command.Parameters.AddWithValue("@CreatedAt", createdAt)
                    command.Parameters.AddWithValue("Deadline", deadline)
                    command.Parameters.AddWithValue("Grade", grade)
                    command.Parameters.AddWithValue("TeacherNO", teacherNo)

                    command.ExecuteNonQuery()
                Catch ex As Exception
                    Return False
                End Try
            End Using
        End Using

        ' test onlyv
        Dim form As New ViewClassCourse("", "", "", "", "")
        form.todosPanel.Controls.Clear()
        form.todosPanel.Invalidate()
        form.todosPanel.Update()

        Return True
    End Function
    Public Function DeleteClass(tableName As String, courseId As String) As Boolean
        Dim query As String = $"delete from {tableName}_Teacher_Courses where CourseId = {courseId}"
        Try
            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Using command As MySqlCommand = GetCommand(query)
                    command.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MsgBox($"Error: {ex}")
            Return False
        End Try
    End Function
    Public Function DeleteStudentClass(tableName As String, courseId As String) As Boolean
        Dim query As String = $"delete from {tableName}_Student_Courses where CourseId = {courseId}"
        Try
            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Using command As MySqlCommand = GetCommand(query)
                    command.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MsgBox($"Error: {ex}")
            Return False
        End Try
    End Function
    Public Function DeleteTeacherTodo() As Boolean
        Dim query As String = ""
        Try
            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Using command As MySqlCommand = GetCommand(query)
                    command.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MsgBox($"Error: {ex}")
            Return False
        End Try
    End Function
    Public Function DeleteTodoClass() As Boolean
        Dim query As String = "DELETE FROM 2200648_Teacher_Todo"
        Try
            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Using command As MySqlCommand = GetCommand(query)
                    command.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MsgBox($"Error: {ex.Message}")
            Return False
        End Try
    End Function

    Public Function UpdateGrade(username As String, id As String, newGrade As String) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"UPDATE {usernameParse}_Student_Passed_Todos SET Grade = @NewGrade WHERE TodoID = @TodoID"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                command.Parameters.AddWithValue("@NewGrade", newGrade)
                command.Parameters.AddWithValue("@TodoID", id)

                Try
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Using
        End Using
        End
    End Function
    Public Function UpdateTeacherGrade(username As String, id As String, newGrade As String) As Boolean
        Dim usernameParse = Handlers.IdParser(username)
        Dim query As String = $"UPDATE {usernameParse}_Teacher_Todo SET Grade = @NewGrade WHERE TodoID = @TodoID"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()
            Using command As MySqlCommand = GetCommand(query)
                command.Parameters.AddWithValue("@NewGrade", newGrade)
                command.Parameters.AddWithValue("@TodoID", id)
                Try
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Using
        End Using
    End Function
    Public Function FetchTeacherClass(tableName As String) As List(Of [Class])
        Dim courses As New List(Of [Class])
        Dim query As String = $"SELECT * FROM {tableName}_Teacher_Courses LIMIT 4;"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()

            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    Try
                        While reader.Read()
                            Dim course As New [Class]() With {
                            .CourseID = reader("CourseID").ToString(),
                            .ClassCode = reader("ClassCode").ToString(),
                            .CourseProgram = reader("CourseProgram").ToString(),
                            .CourseName = reader("CourseName").ToString(),
                            .CourseDescription = reader("CourseDescription").ToString()
                        }
                            courses.Add(course)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using

        Return courses
    End Function
    Public Function FetchAvailableAnnouncement() As List(Of Announcement)
        Dim annoucements As New List(Of Announcement)
        Dim query As String = $"SELECT * FROM Annoucements LIMIT 2;"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()

            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    Try
                        While reader.Read()
                            Dim annoucement As New Announcement() With {
                            .AnnouncementID = reader("AnnouncementID").ToString(),
                            .AnnouncementTitle = reader("AnnouncmentTitle").ToString(),
                            .Description = reader("Description").ToString(),
                            .TeacherNo = reader("TeacherNo").ToString()
                        }
                            annoucements.Add(annoucement)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using
        Return annoucements
    End Function
    Public Function FetchAvailableTodo(tableName As String) As List(Of Todo)
        Dim todos As New List(Of Todo)
        Dim query As String = $"SELECT * FROM {tableName}_Teacher_Todo LIMIT 2;"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()

            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    Try
                        While reader.Read()
                            Dim todo As New Todo() With {
                            .TodoID = reader("TodoID").ToString(),
                            .Title = reader("Title").ToString(),
                            .Description = reader("Description").ToString(),
                            .Filename = reader("Filename").ToString(),
                            .Filepath = reader("Filepath").ToString(),
                            .CreatedAt = reader("CreatedAt").ToString(),
                            .Deadline = reader("Deadline").ToString(),
                            .Grade = reader("Grade").ToString(),
                            .TeacherNo = reader("TeacherNO").ToString
                        }
                            todos.Add(todo)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using

        Return todos
    End Function
    Public Function FetchAllStudentClassCourse(tableName As String) As List(Of StudentClassCourse)
        Dim courses As New List(Of StudentClassCourse)
        Dim query As String = $"SELECT * FROM {tableName}_Student_Courses;"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()

            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    Try
                        While reader.Read()
                            Dim course As New StudentClassCourse() With {
                            .CourseID = reader("CourseID").ToString(),
                            .ClassCode = reader("ClassCode").ToString(),
                            .CourseProgram = reader("CourseProgram").ToString(),
                            .CourseName = reader("CourseName").ToString(),
                            .CourseDescription = reader("CourseDescription").ToString(),
                            .CourseUnits = reader("CourseUnits").ToString(),
                            .CourseTeacher = reader("TeacherName").ToString()
                        }
                            courses.Add(course)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using

        Return courses
    End Function
    Public Function FetchAllTeacherClass(tableName As String) As List(Of [Class])
        Dim courses As New List(Of [Class])
        Dim query As String = $"SELECT * FROM {tableName}_Teacher_Courses;"

        Using connection As MySqlConnection = GetConnection()
            connection.Open()

            Using command As MySqlCommand = GetCommand(query)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    Try
                        While reader.Read()
                            Dim course As New [Class]() With {
                            .CourseID = reader("CourseID").ToString(),
                            .ClassCode = reader("ClassCode").ToString(),
                            .CourseProgram = reader("CourseProgram").ToString(),
                            .CourseName = reader("CourseName").ToString(),
                            .CourseDescription = reader("CourseDescription").ToString()
                        }
                            courses.Add(course)
                        End While
                    Catch ex As Exception
                        MsgBox($"Error: {ex}")
                    End Try
                End Using
            End Using
        End Using

        Return courses
    End Function
    Public Function FetchStudentName(studentId As Integer) As String
        Dim studentName As String = String.Empty
        Dim query As String = $"SELECT FirstName, LastName FROM Students WHERE StudentNo = {studentId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim firstNameIndex As Integer = reader.GetOrdinal("FirstName")
                        Dim lastNameIndex As Integer = reader.GetOrdinal("LastName")

                        If Not reader.IsDBNull(firstNameIndex) AndAlso Not reader.IsDBNull(lastNameIndex) Then
                            Dim firstName As String = reader.GetString(firstNameIndex)
                            Dim lastName As String = reader.GetString(lastNameIndex)
                            studentName = $"{firstName} {lastName}"
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return studentName
    End Function
    Public Function FetchStudentFirstName(studentId As Integer) As String
        Dim studentFirstName As String = String.Empty
        Dim query As String = $"SELECT FirstName FROM Students WHERE StudentNo = {studentId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim firstNameIndex As Integer = reader.GetOrdinal("FirstName")

                        If Not reader.IsDBNull(firstNameIndex) Then
                            Dim firstName As String = reader.GetString(firstNameIndex)
                            studentFirstName = firstName
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return studentFirstName
    End Function
    Public Function FetchStudentYear(studentId As Integer) As String
        Dim studentYear As String = String.Empty
        Dim query As String = $"SELECT Year FROM Students WHERE StudentNo = {studentId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim yearIndex As Integer = reader.GetOrdinal("Year")

                        If Not reader.IsDBNull(yearIndex) Then
                            Dim year As String = reader.GetString(yearIndex)
                            studentYear = Handlers.YearLevel(year)
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return studentYear
    End Function
    Public Function FetchStudentSemester(studentId As Integer) As String
        Dim studentSemester As String = String.Empty
        Dim query As String = $"SELECT Semester FROM Students WHERE StudentNo = {studentId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim semesterIndex As Integer = reader.GetOrdinal("Semester")

                        If Not reader.IsDBNull(semesterIndex) Then
                            Dim semester As String = reader.GetString(semesterIndex)
                            studentSemester = semester
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return studentSemester
    End Function
    Public Function FetchTeacherName(teacherId As Integer) As String
        Dim teacherName As String = String.Empty
        Dim query As String = $"SELECT FirstName, LastName FROM Teachers WHERE TeacherNo = {teacherId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim firstNameIndex As Integer = reader.GetOrdinal("FirstName")
                        Dim lastNameIndex As Integer = reader.GetOrdinal("LastName")

                        If Not reader.IsDBNull(firstNameIndex) AndAlso Not reader.IsDBNull(lastNameIndex) Then
                            Dim firstName As String = reader.GetString(firstNameIndex)
                            Dim lastName As String = reader.GetString(lastNameIndex)
                            teacherName = $"{firstName} {lastName}"
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return teacherName
    End Function
    Public Function FetchTeacherPosition(teacherId As Integer) As String
        Dim teacherPosition As String = String.Empty
        Dim query As String = $"SELECT Position FROM Teachers WHERE TeacherNo = {teacherId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim positionIndex As Integer = reader.GetOrdinal("Position")

                        If Not reader.IsDBNull(positionIndex) Then
                            Dim position As String = reader.GetString(positionIndex)
                            teacherPosition = position
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return teacherPosition
    End Function
    Public Function FetchTeacherDeparment(teacherId As Integer) As String
        Dim teacherDepartment As String = String.Empty
        Dim query As String = $"SELECT Department FROM Teachers WHERE TeacherNo = {teacherId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim departmentIndex As Integer = reader.GetOrdinal("Department")

                        If Not reader.IsDBNull(departmentIndex) Then
                            Dim department As String = reader.GetString(departmentIndex)
                            teacherDepartment = department
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return teacherDepartment
    End Function
    Public Function FetchTeacherFirstName(teacherId As Integer) As String
        Dim teacherFirstName As String = String.Empty
        Dim query As String = $"SELECT FirstName FROM Teachers WHERE TeacherNo = {teacherId}"

        Using connection As MySqlConnection = GetConnection()
            Using command As MySqlCommand = GetCommand(query)
                Try
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim firstNameIndex As Integer = reader.GetOrdinal("FirstName")

                        If Not reader.IsDBNull(firstNameIndex) Then
                            Dim firstName As String = reader.GetString(firstNameIndex)
                            teacherFirstName = firstName
                        Else
                            Console.WriteLine("Error")
                        End If
                    Else
                        Console.WriteLine("Error")
                    End If
                Catch ex As Exception
                    Console.WriteLine($"Error: {ex.Message}")
                End Try
            End Using
        End Using

        Return teacherFirstName
    End Function
End Module