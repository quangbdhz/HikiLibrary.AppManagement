CREATE DATABASE Library_Management
GO

USE Library_Management
GO

CREATE TABLE Gender
(
	Id INT IDENTITY (1,1) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE StatusChangePass
(
	Id INT IDENTITY (1,1) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	CountDelete INT DEFAULT 0
)
GO


CREATE TABLE Status
(
	Id INT IDENTITY (1,1) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE AuthorityHuman 
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE AuthorityStaff
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE Human
(
	Id INT IDENTITY (1,1) PRIMARY KEY,
	MS NVARCHAR(100) NOT NULL,
	DisplayName NVARCHAR(1000) NOT NULL,
	DateOfBirth DATETIME NOT NULL,
	IdGender INT NOT NULL,
	IdAuthorityHuman INT NOT NULL,
	Address NVARCHAR(500) NOT NULL,
	Phone NVARCHAR(20) NOT NULL,
	Email NVARCHAR(200) NOT NULL,
	UrlAvatarHuman NVARCHAR(1000) NOT NULL,
	Forfeit FLOAT DEFAULT 0,
	PayFine FLOAT DEFAULT 0, 
	Compensation FLOAT DEFAULT 0, 
	Score FLOAT DEFAULT 0, 
	Note NVARCHAR(max),
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdGender_Human_TO_Id_Gender FOREIGN KEY (IdGender) REFERENCES dbo.Gender(Id),
	CONSTRAINT FK_IdAuthorityHuman_Human_TO_Id_AuthorityHuman FOREIGN KEY (IdAuthorityHuman) REFERENCES dbo.AuthorityHuman(Id)
)
GO

CREATE TABLE UserHuman
(
	Id INT IDENTITY (1,1) PRIMARY KEY,
	IdHuman INT NOT NULL,
	UserName NVARCHAR(100) NOT NULL,
	Password NVARCHAR(300),
	DateInitPass DATETIME,
	IdStatusChangePass INT NOT NULL DEFAULT 0, 	
	DatePasswordChange DATETIME,
	Note NVARCHAR(max),
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdHuman_UserHuman_TO_Id_Human FOREIGN KEY (IdHuman) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdStatusChangePass_UserHuman_TO_Id_StatusChangePass FOREIGN KEY (IdStatusChangePass) REFERENCES dbo.StatusChangePass(Id)
)
GO

CREATE TABLE UserStaff
(
	Id INT IDENTITY (1,1) PRIMARY KEY,
	IdHuman INT NOT NULL,
	UserName NVARCHAR(100) NOT NULL,
	Password NVARCHAR(300) NOT NULL,
	IdAuthorityStaff INT NOT NULL,
	Note NVARCHAR(max),
	ScoreInputBook INT DEFAULT 0,
	ScoreOuputBook INT DEFAULT 0,
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdHuman_UserStaff_TO_Id_Human FOREIGN KEY (IdHuman) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdAuthorityStaff_Users_TO_Id_Authority FOREIGN KEY (IdAuthorityStaff) REFERENCES dbo.AuthorityStaff(Id)
)
GO

CREATE TABLE BookSubject
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(1000) NOT NULL,
	ScoreInputSubject INT DEFAULT 0,
	ScoreOuputSubject INT DEFAULT 0,
	Note NVARCHAR(max),
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE Language
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	Note NVARCHAR(max),
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE Publisher
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(1000) NOT NULL,
	Address NVARCHAR(300) NOT NULL,
	Phone NVARCHAR(20) NOT NULL,
	Email NVARCHAR(200) NOT NULL, 
	Note NVARCHAR(max),
	Score FLOAT DEFAULT 0, 
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE Author
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(max) NOT NULL,
	Address NVARCHAR(max),
	Phone NVARCHAR(100),
	Email NVARCHAR(200), 
	Note NVARCHAR(max),
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE Book
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(1000) NOT NULL,
	BookPrice FLOAT DEFAULT 0,
	BorrowingIdHuman INT NOT NULL,
	BookSubject NVARCHAR(500) NOT NULL,
	Author NVARCHAR(500) NOT NULL,
	IdLanguage INT NOT NULL,
	IdPublisher INT,
	IdStatus INT NOT NULL,
	IdStatusReturnBookToHuman INT NOT NULL,
	LibraryDateBorrowed DATETIME NOT NULL,
	LibraryDueDate DATETIME NOT NULL,
	Color NVARCHAR(30) NOT NULL,
	DateReturnBookToHuman DATETIME NOT NULL,
	UrlImageBook NVARCHAR(500) NOT NULL,
	Note NVARCHAR(max),
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_BorrowingIdHuman_Book_TO_Id_Human FOREIGN KEY (BorrowingIdHuman) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdLanguage_Book_TO_Id_Language FOREIGN KEY (IdLanguage) REFERENCES dbo.Language(Id),
	CONSTRAINT FK_IdPublisher_Book_TO_Id_Publisher FOREIGN KEY (IdPublisher) REFERENCES dbo.Publisher(Id),
	CONSTRAINT FK_IdStatus_Book_TO_Id_Status FOREIGN KEY (IdStatus) REFERENCES dbo.Status(Id)
)
GO

CREATE TABLE Fined
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE BorrowBooks
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdHuman INT NOT NULL,
	IdBook INT NOT NULL,
	DateBorrowed DATETIME NOT NULL,
	DueDate DATETIME NOT NULL,
	IdStatus INT NOT NULL,
	IdFined INT NOT NULL,
	Color NVARCHAR(30),
	ContractualFine FLOAT NOT NULL DEFAULT 0,
	Note NVARCHAR(max),
	CountDelete INT DEFAULT 0
	
	CONSTRAINT FK_IdHuman_BorrowBooks_TO_Id_Human FOREIGN KEY (IdHuman) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdBook_BorrowBooks_TO_Id_Book FOREIGN KEY (IdBook) REFERENCES dbo.Book(Id),
	CONSTRAINT FK_IdStatus_BorrowBooks_TO_Id_Status FOREIGN KEY (IdStatus) REFERENCES dbo.Status(Id),
	CONSTRAINT FK_IdFined_BorrowBooks_TO_Id_Fined FOREIGN KEY (IdFined) REFERENCES dbo.Fined(Id)
)
GO

CREATE TABLE TimeLine
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	TimeStart TIME NOT NULL,
	EndTime TIME NOT NULL,
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE TimeTable
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdTimeLine INT NOT NULL,
	IdAuthorityStaff INT NOT NULL,

	IdHumanSunday INT NOT NULL,
	CheckIdHumanSundayWork NVARCHAR(100) DEFAULT NULL, 

	IdHumanMonday INT NOT NULL,
	CheckIdHumanMondayWork NVARCHAR(100) DEFAULT NULL, 

	IdHumanTuesday INT NOT NULL,
	CheckIdHumanTuesdayWork NVARCHAR(100) DEFAULT NULL, 

	IdHumanWednesday INT NOT NULL,
	CheckIdHumanWednesdayWork NVARCHAR(100) DEFAULT NULL, 

	IdHumanThursday INT NOT NULL,
	CheckIdHumanThursdayWork NVARCHAR(100) DEFAULT NULL, 

	IdHumanFriday INT NOT NULL,
	CheckIdHumanFridayWork NVARCHAR(100) DEFAULT NULL, 

	IdHumanSaturday INT NOT NULL,
	CheckIdHumanSaturdayWork NVARCHAR(100) DEFAULT NULL, 

	CountDelete INT DEFAULT 0,
	InnitiatedDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL


	CONSTRAINT FK_IdTimeLine_TimeTable_TO_Id_TimeLine FOREIGN KEY (IdTimeLine) REFERENCES dbo.TimeLine(Id),
	CONSTRAINT FK_IdAuthorityStaff_TimeTable_TO_Id_AuthorityStaff FOREIGN KEY (IdAuthorityStaff) REFERENCES dbo.AuthorityStaff(Id),
	CONSTRAINT FK_IdHumanSunday_TimeTable_TO_Id_Human FOREIGN KEY (IdHumanSunday) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdHumanMonday_TimeTable_TO_Id_Human FOREIGN KEY (IdHumanMonday) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdHumanTuesday_TimeTable_TO_Id_Human FOREIGN KEY (IdHumanTuesday) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdHumanWednesday_TimeTable_TO_Id_Human FOREIGN KEY (IdHumanWednesday) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdHumanThursday_TimeTable_TO_Id_Human FOREIGN KEY (IdHumanThursday) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdHumanFriday_TimeTable_TO_Id_Human FOREIGN KEY (IdHumanFriday) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdHumanSaturday_TimeTable_TO_Id_Human FOREIGN KEY (IdHumanSaturday) REFERENCES dbo.Human(Id)
)
GO

CREATE TABLE HistoryCreateTimeTable
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdUserStaff INT NOT NULL,
	DisplayName NVARCHAR(2000) NOT NULL,
	DateCreate DATETIME NOT NULL

	CONSTRAINT FK_IdUserStaff_HistoryCreateTimeTable_TO_Id_UserStaff FOREIGN KEY (IdUserStaff) REFERENCES dbo.UserStaff(Id)
)
GO

CREATE TABLE SoftwareFilePath
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	FilePathName NVARCHAR(500) NOT NULL
)
GO

CREATE TABLE StatusBill
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	CountDelete INT DEFAULT 0
)
GO

CREATE TABLE BillBookOfHuman 
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdHuman INT NOT NULL,
	IdStaff INT NOT NULL,
	BorrowedDate DATETIME NOT NULL,
	DateOfRepayment DATETIME NOT NULL,
	Note NVARCHAR(max), 
	IdStatusBill INT NOT NULL,
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdHuman_BillBookOfHuman_TO_Id_Human FOREIGN KEY (IdHuman) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdStaff_BillBookOfHuman_TO_Id_UserStaff FOREIGN KEY (IdStaff) REFERENCES dbo.UserStaff(Id),
	CONSTRAINT FK_IdStatusBill_BillBookOfHuman_TO_Id_StatusBill FOREIGN KEY (IdStatusBill) REFERENCES dbo.StatusBill(Id)

)
GO

CREATE TABLE ListBookLibraryBorrowHuman
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdBillBookOfHuman INT NOT NULL,
	IdBook INT NOT NULL,
	NumberOfBooks INT NOT NULL,
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdBook_ListBookLibraryBorrowHuman_TO_Id_Book FOREIGN KEY (IdBook) REFERENCES dbo.Book(Id),
	CONSTRAINT FK_IdBillBookOfHuman_ListBookLibraryBorrowHuman_TO_IdBillBookOfHuman_ListBookLibraryBorrowHuman 
		FOREIGN KEY (IdBillBookOfHuman) REFERENCES dbo.BillBookOfHuman(Id)
)
GO


CREATE TABLE BillBookOfCustomer  
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdHuman INT NOT NULL,
	IdStaff INT NOT NULL,
	BorrowedDate DATETIME NOT NULL,
	DateOfRepayment DATETIME NOT NULL,
	CashReceived FLOAT DEFAULT 0,
	IdStatusBill INT NOT NULL,
	Note NVARCHAR(max), 
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdHuman_BillBookOfCustomer_TO_Id_Human FOREIGN KEY (IdHuman) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdStaff_BillBookOfCustomer_TO_Id_UserStaff FOREIGN KEY (IdStaff) REFERENCES dbo.UserStaff(Id),
	CONSTRAINT FK_IdStatusBill_BillBookOfCustomer_TO_Id_StatusBill FOREIGN KEY (IdStatusBill) REFERENCES dbo.StatusBill(Id)
)
GO

CREATE TABLE ListBookCustomerBorrow
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdBillBookOfCustomer INT  NOT NULL,
	IdBook INT NOT NULL,
	NumberOfBooks INT NOT NULL,
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdBook_ListBookCustomerBorrow_TO_Id_Book FOREIGN KEY (IdBook) REFERENCES dbo.Book(Id),
	CONSTRAINT FK_IdBillBookOfCustomer_ListBookCustomerBorrow_TO_Id_BillBookOfCustomer
		FOREIGN KEY (IdBillBookOfCustomer) REFERENCES dbo.BillBookOfCustomer(Id)
)
GO

CREATE TABLE BillCustomerReturnBookLibrary   
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdHuman INT NOT NULL,
	IdStaff INT NOT NULL,
	DateOfRepayment DATETIME NOT NULL,
	AllTheDepositAmount FLOAT DEFAULT 0,
	CashReceived FLOAT DEFAULT 0,
	CustomerFined FLOAT DEFAULT 0,
	AllFines FLOAT DEFAULT 0,
	IdStatusBill INT NOT NULL,
	Note NVARCHAR(max), 
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdHuman_BillCustomerReturnBookLibrary_TO_Id_Human FOREIGN KEY (IdHuman) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdStaff_BillCustomerReturnBookLibrary_TO_Id_UserStaff FOREIGN KEY (IdStaff) REFERENCES dbo.UserStaff(Id),
	CONSTRAINT FK_IdStatusBill_BillCustomerReturnBookLibrary_TO_Id_StatusBill FOREIGN KEY (IdStatusBill) REFERENCES dbo.StatusBill(Id)
)
GO


CREATE TABLE ListBookCustomerReturnBookLibrary
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdBillCustomerReturnBookLibrary  INT NOT NULL,
	IdBook INT NOT NULL,
	NumberOfBooks INT NOT NULL,
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdBook_ListBookCustomerReturnBookLibrary_TO_Id_Book FOREIGN KEY (IdBook) REFERENCES dbo.Book(Id),
	CONSTRAINT FK_IdBillCustomerReturnBookLibrary_ListBookCustomerReturnBookLibrary_TO_Id_BillCustomerReturnBookLibrary
		FOREIGN KEY (IdBillCustomerReturnBookLibrary) REFERENCES dbo.BillCustomerReturnBookLibrary(Id)
)
GO


CREATE TABLE BillReturnBookHuman   
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdHuman INT NOT NULL,
	IdStaff INT NOT NULL,
	DateOfRepayment DATETIME NOT NULL,
	IdStatusBill INT NOT NULL,
	LibraryFined FLOAT DEFAULT 0,
	Note NVARCHAR(max), 
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdHuman_BillReturnBookHuman_TO_Id_Human FOREIGN KEY (IdHuman) REFERENCES dbo.Human(Id),
	CONSTRAINT FK_IdStaff_BillReturnBookHuman_TO_Id_UserStaff FOREIGN KEY (IdStaff) REFERENCES dbo.UserStaff(Id),
	CONSTRAINT FK_IdStatusBill_BillReturnBookHuman_TO_Id_StatusBill FOREIGN KEY (IdStatusBill) REFERENCES dbo.StatusBill(Id)
)
GO

CREATE TABLE ListReturnBookHuman
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdBillReturnBookHuman  INT NOT NULL,
	IdBook INT NOT NULL,
	NumberOfBooks INT NOT NULL,
	CountDelete INT DEFAULT 0

	CONSTRAINT FK_IdBook_ListReturnBookHuman_TO_Id_Book FOREIGN KEY (IdBook) REFERENCES dbo.Book(Id),
	CONSTRAINT FK_IdBillReturnBookHuman_ListReturnBookHuman_TO_Id_BillReturnBookHuman
		FOREIGN KEY (IdBillReturnBookHuman) REFERENCES dbo.BillReturnBookHuman(Id)
)
GO

CREATE TABLE ListDetleImage
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	UrlImageDelete NVARCHAR(1000) NOT NULL
)
GO

CREATE TABLE LibraryRegulations
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	LibraryTimeToBorrowBooks NVARCHAR(50) NOT NULL,
	TimeCustomersBorrowBooks NVARCHAR(50) NOT NULL	
)

GO
INSERT INTO AuthorityHuman VALUES
(N'Student', 0),
(N'Teacher', 0),
(N'Staff', 0),
(N'Manager', 0),
(N'Director', 0),
(N'Chairman', 0);

INSERT INTO StatusChangePass VALUES
(N'Chưa đổi mật khẩu', 0),
(N'Đã đổi mật khẩu', 0);

INSERT INTO Gender VALUES
(N'Male', 0),
(N'Female', 0),
(N'Custom', 0);

INSERT INTO Status VALUES
(N'Chưa trả', 0),
(N'Đã trả', 0),
(N'Bị mất', 0),
(N'Cho mượn', 0);

INSERT INTO StatusBill VALUES
(N'Chưa In Hóa Đơn', 0),
(N'Đã In Hóa Đơn', 0);

INSERT INTO Language VALUES
(N'Tiếng Việt', null, 0),
(N'Tiếng Anh', N'English', 0),
(N'Tiếng Nhật', N'日本語', 0);

INSERT INTO Fined VALUES
(N'Không bị phạt', 0),
(N'Bị phạt', 0);

INSERT INTO BookSubject VALUES
(N'Textbook', 0, 0, N'Sách giáo khoa', 0),
(N'Magazine', 0, 0, N'Tạp chí(phổ thông)', 0),
(N'Autobiography', 0, 0, N'Cuốn tự truyện', 0),
(N'Journals', 0, 0, N'Tạp chí, báo hằng ngày', 0),
(N'Encyclopedia', 0, 0, N'Bách khoa toàn thư', 0),
(N'Thriller', 0, 0, N'Sách trinh thám', 0),
(N'Dictionary', 0, 0, N'Từ điển', 0),
(N'Short story', 0, 0, N'Truyện ngắn', 0),
(N'Novel', 0, 0, N'Tiểu thuyết', 0),
(N'Comic', 0, 0, N'Truyện tranh', 0),
(N'Psychology', 0, 0, N'Tâm lý học', 0),
(N'Exercise book', 0, 0, N'Sách bài tập', 0);

INSERT INTO TimeLine VALUES
('07:00:00', '11:00:00', 0),
('12:00:00', '16:00:00', 0),
('16:00:00', '20:00:00', 0);

INSERT INTO Publisher VALUES
(N'Nhà xuất bản Kim Đồng', N'55 Quang Trung, Hai Bà Trưng, Hà Nội', N'02439434730', N'kimdong@hn.vnn.vn', N'Website: http://www.nxbkimdong.com.vn/', 0, 0),
(N'Nhà xuất bản Trẻ', N'161B Lý Chính Thắng, Phường 7, Quận 3, Thành phố Hồ Chí Minh', N'02839316289', N'hopthubandoc@nxbtre.com.vn', N'Website: http://www.nxbtre.com.vn', 0, 0),
(N'Nhà xuất bản Tổng hợp thành phố Hồ Chí Minh', N'62 Nguyễn Thị Minh Khai, Phường Đa Kao, Quận 1, TP.HCM', N'02838225340', N'tonghop@nxbhcm.com.vn', N'Website: http://www.nxbhcm.com.vn', 0, 0),
(N'Nhà xuất bản chính trị quốc gia sự thật', N'Số 24 Quang Trung, Hoàn Kiếm, Hà Nội', N'02438221633', N'suthat@nxbctqg.vn', N'Website: http://www.nxbctqg.org.vn', 0, 0),
(N'Nhà xuất bản giáo dục', N'81 Trần Hưng Đạo, Hà Nội', N'02438220801', N' ', N'Website: http://www.nxbgd.vn', 0, 0),
(N'Nhà xuất bản Hội Nhà văn', N'Số 65 Nguyễn Du, Hà Nội', N'02438222135', N' ', N'Website: http://www.nxbhoinhavan.net/', 0, 0);

INSERT INTO AuthorityStaff VALUES
(N'Manager', 0),
(N'Librarian', 0),
(N'Arranger', 0),
(N'Admin', 2);

INSERT INTO Author VALUES
(N'Wataru Watari', N'Japan', N'0888888888', N'oregairu@ja.pan', N'', 0),
(N'Kawahara Reki', N'Japan', N'0131147147', N'SAO@ja.pan', N'', 0),
(N'Shinkai Makoto', N'Japan', N'0137136133', N'shinkai@ja.pan', N'', 0),
(N'Naoshi Arakawa', N'Japan', N'0127361371', N'shigatsuwakiminouso@ja.pan', N'', 0),
(N'Nguyễn Nhật Ánh', N'Việt Nam', N'0137161461', N'anh91.giayen.thienkhoihcm@gmail.com', N'', 0);


INSERT INTO Human VALUES
(N'999999', N'Tran Quang', '20011008', 1, 6, N'Marvel Comic', N'111111111', N'quangbdhz@gmail.com', N'F:\Library_Management\Library_Management\Library_Management\bin\Debug\DataImageCustomer\hachiman.png', 0, 0, 0, 0, N'', 0);

INSERT INTO UserHuman VALUES
(1, N'999999', N'30fdf15fd513fd69085f9344ff2d5d716254aa367bcac88e78ee60ad0298d606', '20211022', 1, null, null, 0 );

INSERT INTO UserStaff VALUES
(1, N'admin', N'30fdf15fd513fd69085f9344ff2d5d716254aa367bcac88e78ee60ad0298d606', 4, N'test admin', 0, 0, 2);

CREATE TRIGGER tg_changePassword_UserStaff on UserStaff FOR UPDATE AS
DECLARE @new_pwd AS NVARCHAR(300), @id AS INT, @old_pwd NVARCHAR(300)
SELECT @new_pwd = new.Password, @id = new.id, @old_pwd = old.Password FROM INSERTED AS new, DELETED AS old WHERE new.id = old.id
IF(@new_pwd != @old_pwd)
BEGIN
	UPDATE UserStaff SET Password = @new_pwd WHERE id = @id
END
ELSE
BEGIN
	PRINT N'Trùng mật khẩu cũ'
	ROLLBACK TRANSACTION
END

CREATE TRIGGER tg_statusChangePwd_UserHuman on UserHuman FOR UPDATE AS
DECLARE @new_pwd AS NVARCHAR(300), @id AS INT, @old_pwd NVARCHAR(300)
SELECT @new_pwd = new.Password, @id = new.id, @old_pwd = old.Password, @old_idStatusChangePass = old.IdStatusChangePass FROM INSERTED AS new, DELETED AS old WHERE new.id = old.id
IF(@new_pwd != @old_pwd)
BEGIN
	IF(@old_idStatusChangePass = 1)
	BEGIN
		UPDATE UserHuman SET IdStatusChangePass = 2 WHERE id = @id
	END
END

CREATE FUNCTION load_data_Author (@name NVARCHAR(max))
RETURNS TABLE
AS 
RETURN SELECT * FROM Author WHERE DisplayName = @name


CREATE PROCEDURE load_data_AuthorityHuman
AS
BEGIN
	SELECT * FROM AuthorityHuman
END


CREATE PROCEDURE load_data_AuthorityStaff
AS
BEGIN
	SELECT * FROM AuthorityStaff
END


CREATE PROCEDURE load_data_BillBookOfCustomer
AS
BEGIN
	SELECT * FROM BillBookOfCustomer
END


CREATE PROCEDURE load_data_BillBookOfHuman
AS
BEGIN
	SELECT * FROM BillBookOfHuman
END


CREATE PROCEDURE load_data_BillCustomerReturnBook
AS
BEGIN
	SELECT * FROM BillCustomerReturnBook
END


CREATE PROCEDURE load_data_BillReturnBookHuman
AS
BEGIN
	SELECT * FROM BillReturnBookHuman
END


CREATE PROCEDURE load_data_Book
AS
BEGIN
	SELECT * FROM Book
END


CREATE FUNCTION load_data_BookSubject (@name NVARCHAR(max))
RETURNS TABLE
AS 
RETURN SELECT * FROM BookSubject WHERE DisplayName = @name


CREATE PROCEDURE load_data_BorrowBook
AS
BEGIN
	SELECT * FROM BorrowBook
END

CREATE PROCEDURE load_data_HistoryCreateTimeTable
AS
BEGIN
	SELECT * FROM HistoryCreateTimeTable
END


CREATE PROCEDURE load_data_Human
AS
BEGIN
	SELECT * FROM Human
END


CREATE PROCEDURE load_data_ListBookCustomerBorrow
AS
BEGIN
	SELECT * FROM ListBookCustomerBorrow
END

CREATE PROCEDURE load_data_ListBookCustomerReturnBookLibrary
AS
BEGIN
	SELECT * FROM ListBookCustomerReturnBookLibrary
END

CREATE PROCEDURE load_data_ListBookLibraryBorrow
AS
BEGIN
	SELECT * FROM ListBookLibraryBorrow
END


CREATE PROCEDURE load_data_ListReturnBookHuman
AS
BEGIN
	SELECT * FROM ListReturnBookHuman
END


CREATE PROCEDURE load_data_Publishher
AS
BEGIN
	SELECT * FROM Publishher
END


CREATE PROCEDURE load_data_Status
AS
BEGIN
	SELECT * FROM Status
END


CREATE PROCEDURE load_data_TimeTable
AS
BEGIN
	SELECT * FROM TimeTable
END


CREATE PROC Add_TimeTable
@IdTimeLine INT,
@IdAuthorityStaff INT,
@IdHumanSunday INT,
	@CheckIdHumanSundayWork NVARCHAR(100), 
	@IdHumanMonday INT,
	@CheckIdHumanMondayWork NVARCHAR(100), 
	@IdHumanTuesday INT,
	@CheckIdHumanTuesdayWork NVARCHAR(100), 
	@IdHumanWednesday INT,
	@CheckIdHumanWednesdayWork NVARCHAR(100), 
	@IdHumanThursday INT,
	@CheckIdHumanThursdayWork NVARCHAR(100), 
	@IdHumanFriday INT,
	@CheckIdHumanFridayWork NVARCHAR(100), 
	@IdHumanSaturday INT,
	@CheckIdHumanSaturdayWork NVARCHAR(100), 
	@CountDelete INT,
	@InnitiatedDate DATETIME,
	@EndDate DATETIME

AS
INSERT INTO TimeTable 
VALUES(@IdTimeLine,@IdAuthorityStaff,
@IdHumanSunday,
	@CheckIdHumanSundayWork, 
	@IdHumanMonday,
	@CheckIdHumanMondayWork, 
	@IdHumanTuesday,
	@CheckIdHumanTuesdayWork, 
	@IdHumanWednesday,
	@CheckIdHumanWednesdayWork, 
	@IdHumanThursday,
	@CheckIdHumanThursdayWork, 
	@IdHumanFriday,
	@CheckIdHumanFridayWork, 
	@IdHumanSaturday,
	@CheckIdHumanSaturdayWork, 
	@CountDelete,
	@InnitiatedDate,
	@EndDate)

CREATE PROC Add_Human
	@MS NVARCHAR(100),
	@DisplayName NVARCHAR(1000),
	@DateOfBirth DATETIME,
	@IdGender INT,
	@Address NVARCHAR(500),
	@Phone NVARCHAR(20),
	@Email NVARCHAR(200),
	@UrlAvatarHuman NVARCHAR(1000),
	@Forfeit FLOAT,
	@PayFine FLOAT, 
	@Compensation FLOAT, 
	@Score FLOAT, 
	@Note NVARCHAR(max),
	@CountDelete INT
AS
INSERT INTO Human 
VALUES(@MS,
	@DisplayName,
	@DateOfBirth,
	@IdGender,
	6,
	@Address,
	@Phone,
	@Email,
	@UrlAvatarHuman,
	@Forfeit,
	@PayFine, 
	@Compensation, 
	@Score, 
	@Note,
	@CountDelete)

	-- User Human (tài khoản người dùng)
CREATE PROC Add_UserHuman
	@IdHuman INT,
	@UserName NVARCHAR(100),
	@Password NVARCHAR(300),
	@DateInitPass DATETIME,
	@IdStatusChangePass INT, 	
	@DatePasswordChange DATETIME,
	@Note NVARCHAR(max),
	@CountDelete INT
AS
INSERT INTO UserHuman 
VALUES(@IdHuman,
	@UserName,
	@Password,
	@DateInitPass,
	@IdStatusChangePass, 	
	@DatePasswordChange,
	@Note,
	@CountDelete)

	-- User Staff (tài khoản nhân viên)
CREATE PROC Add_UserStaff
	@IdHuman INT,
	@UserName NVARCHAR(100),
	@Password NVARCHAR(300),
	@Note NVARCHAR(max),
	@ScoreInputBook INT,
	@ScoreOuputBook INT,
	@CountDelete INT
AS
INSERT INTO UserStaff 
VALUES(@IdHuman,
	@UserName,
	@Password,
	3,
	@Note,
	@ScoreInputBook,
	@ScoreOuputBook,
	@CountDelete)

	-- Author
CREATE PROC Add_Author
	@DisplayName NVARCHAR(max),
	@Address NVARCHAR(max),
	@Phone NVARCHAR(100),
	@Email NVARCHAR(200), 
	@Note NVARCHAR(max),
	@CountDelete INT
AS 
INSERT INTO Author 
VALUES(@DisplayName,
	@Address,
	@Phone,
	@Email, 
	@Note,
	@CountDelete)

	-- Language Book
CREATE PROC Add_Language
	@DisplayName NVARCHAR(100),
	@Note NVARCHAR(max),
	@CountDelete INT
AS
INSERT INTO Language 
VALUES(@DisplayName,
	@Note,
	@CountDelete)

	-- Subject
CREATE PROC Add_BookSubject
	@DisplayName NVARCHAR(1000),
	@ScoreInputSubject INT,
	@ScoreOuputSubject INT,
	@Note NVARCHAR(max),
	@CountDelete INT 
AS 
INSERT INTO BookSubject 
VALUES(@DisplayName,
	@ScoreInputSubject,
	@ScoreOuputSubject,
	@Note,
	@CountDelete)

	-- Publisher
CREATE PROC Add_Publisher
@DisplayName NVARCHAR(1000),
@Address NVARCHAR(300),
@Phone NVARCHAR(20),
	@Email NVARCHAR(200),
	@Note NVARCHAR(max),
	@Score FLOAT,
	@CountDelete INT
AS
INSERT INTO Publisher
VALUES(@DisplayName,
	@Address,
	@Phone,
	@Email,
	@Note,
	@Score,
	@CountDelete)

	-- Book
CREATE PROC Add_Book
	@DisplayName NVARCHAR(1000),
	@BookPrice FLOAT,
	@BorrowingIdHuman INT,
	@BookSubject NVARCHAR(500),
	@Author NVARCHAR(500),
	@IdLanguage INT,
	@IdPublisher INT,
	@IdStatus INT,
	@IdStatusReturnBookToHuman INT,
	@LibraryDateBorrowed DATETIME,
	@LibraryDueDate DATETIME,
	@Color NVARCHAR(30),
	@DateReturnBookToHuman DATETIME,
	@UrlImageBook NVARCHAR(500),
	@Note NVARCHAR(max),
	@CountDelete INT
AS
INSERT INTO Book 
VALUES(@DisplayName,
	@BookPrice,
	@BorrowingIdHuman,
	@BookSubject,
	@Author,
	@IdLanguage,
	@IdPublisher,
	@IdStatus,
	@IdStatusReturnBookToHuman,
	@LibraryDateBorrowed,
	@LibraryDueDate,
	@Color,
	@DateReturnBookToHuman,
	@UrlImageBook,
	@Note,
	@CountDelete)

	-- Borrow Book
CREATE PROC Add_BorrowBooks
	@IdHuman INT,
	@IdBook INT,
	@DateBorrowed DATETIME,
	@DueDate DATETIME,
	@IdStatus INT,
	@IdFined INT,
	@Color NVARCHAR(30),
	@ContractualFine FLOAT,
	@Note NVARCHAR(max),
	@CountDelete INT
AS
INSERT INTO BorrowBooks 
VALUES(@IdHuman,
	@IdBook,
	@DateBorrowed,
	@DueDate,
	@IdStatus,
	@IdFined,
	@Color,
	@ContractualFine,
	@Note,
	@CountDelete)

	-- Bill Of Human (hóa đơn in ra cho đối tác khi cho thư viện mượn sách)
CREATE PROC Add_BillBookOfHuman 
	@IdHuman INT,
	@IdStaff INT,
	@BorrowedDate DATETIME,
	@DateOfRepayment DATETIME,
	@Note NVARCHAR(max), 
	@IdStatusBill INT,
	@CountDelete INT
AS
INSERT INTO BillBookOfHuman 
VALUES(@IdHuman,
	@IdStaff,
	@BorrowedDate,
	@DateOfRepayment,
	@Note, 
	@IdStatusBill,
	@CountDelete)

	-- Bill Of Customer (hóa đơn in ra cho khách hàng khi đến thư viện mượn sách)
CREATE PROC Add_BillBookOfCustomer  
	@IdHuman INT,
	@IdStaff INT,
	@BorrowedDate DATETIME,
	@DateOfRepayment DATETIME,
	@CashReceived FLOAT,
	@IdStatusBill INT,
	@Note NVARCHAR(max), 
	@CountDelete INT 
AS 
INSERT INTO BillBookOfCustomer
VALUES(@IdHuman,
	@IdStaff,
	@BorrowedDate,
	@DateOfRepayment,
	@CashReceived,
	@IdStatusBill,
	@Note, 
	@CountDelete)

	-- Bill Customer Retun Book (hóa đơn in ra cho khách hàng khi trả lại sách cho thư viện)
CREATE PROC Add_BillCustomerReturnBookLibrary   
	@IdHuman INT,
	@IdStaff INT,
	@DateOfRepayment DATETIME,
	@AllTheDepositAmount FLOAT,
	@CashReceived FLOAT,
	@CustomerFined FLOAT,
	@AllFines FLOAT,
	@IdStatusBill INT,
	@Note NVARCHAR(max), 
	@CountDelete INT 
AS
INSERT INTO BillCustomerReturnBookLibrary 
VALUES(@IdHuman,
	@IdStaff,
	@DateOfRepayment,
	@AllTheDepositAmount,
	@CashReceived,
	@CustomerFined,
	@AllFines,
	@IdStatusBill,
	@Note, 
	@CountDelete) 

	-- Bill Return Book Human (hóa đơn in ra cho đối tác khi đối tác nhận lại sách từ thư viện)
CREATE PROC Add_BillReturnBookHuman   
	@IdHuman INT,
	@IdStaff INT,
	@DateOfRepayment DATETIME,
	@IdStatusBill INT,
	@LibraryFined FLOAT,
	@Note NVARCHAR(max), 
	@CountDelete INT
AS
INSERT INTO BillReturnBookHuman 
VALUES(@IdHuman,
	@IdStaff,
	@DateOfRepayment,
	@IdStatusBill,
	@LibraryFined,
	@Note, 
	@CountDelete)

	-- History Create Time Table (lịch sử tạo thời gian biểu làm việc)	
CREATE PROC Add_HistoryCreateTimeTable
	@IdUserStaff INT ,
	@DisplayName NVARCHAR(2000),
	@DateCreate DATETIME
AS
INSERT INTO HistoryCreateTimeTable 
VALUES(@IdUserStaff,
	@DisplayName,
	@DateCreate)

--CẬP NHẬT
	-- Time Table
create proc Update_TimeTable
	@IdTimeLine INT,
	@IdAuthorityStaff INT,
	@IdHumanSunday INT,
	@CheckIdHumanSundayWork NVARCHAR(100), 
	@IdHumanMonday INT,
	@CheckIdHumanMondayWork NVARCHAR(100), 
	@IdHumanTuesday INT,
	@CheckIdHumanTuesdayWork NVARCHAR(100), 
	@IdHumanWednesday INT,
	@CheckIdHumanWednesdayWork NVARCHAR(100), 
	@IdHumanThursday INT,
	@CheckIdHumanThursdayWork NVARCHAR(100), 
	@IdHumanFriday INT,
	@CheckIdHumanFridayWork NVARCHAR(100), 
	@IdHumanSaturday INT,
	@CheckIdHumanSaturdayWork NVARCHAR(100), 
	@CountDelete INT,
	@InnitiatedDate DATETIME,
	@EndDate DATETIME
as
UPDATE TimeTable 
SET IdAuthorityStaff = @IdAuthorityStaff,
	IdHumanSunday = @IdHumanSunday,
	CheckIdHumanSundayWork = @CheckIdHumanSundayWork,
	IdHumanMonday = @IdHumanMonday,
	CheckIdHumanMondayWork = @CheckIdHumanMondayWork,
	IdHumanTuesday = @IdHumanTuesday,
	CheckIdHumanTuesdayWork = @CheckIdHumanTuesdayWork,
	IdHumanWednesday = @IdHumanWednesday,
	CheckIdHumanWednesdayWork = @CheckIdHumanWednesdayWork,
	IdHumanThursday = @IdHumanThursday,
	CheckIdHumanThursdayWork = @CheckIdHumanThursdayWork,
	IdHumanFriday = @IdHumanFriday,
	CheckIdHumanFridayWork = @CheckIdHumanFridayWork,
	IdHumanSaturday = @IdHumanSaturday,
	CheckIdHumanSaturdayWork = @CheckIdHumanSaturdayWork,
	CountDelete = @CountDelete,
	InnitiatedDate = @InnitiatedDate,
	EndDate = @EndDate
WHERE IdTimeLine = @IdTimeLine

	-- Human (Customer / khách hàng)
CREATE PROC Update_Human
	@MS NVARCHAR(100),
	@DisplayName NVARCHAR(1000),
	@DateOfBirth DATETIME,
	@IdGender INT,
	@Address NVARCHAR(500),
	@Phone NVARCHAR(20),
	@Email NVARCHAR(200),
	@UrlAvatarHuman NVARCHAR(1000),
	@Forfeit FLOAT,
	@PayFine FLOAT, 
	@Compensation FLOAT, 
	@Score FLOAT, 
	@Note NVARCHAR(max),
	@CountDelete INT
AS
UPDATE Human 
SET DisplayName=@DisplayName,
	DateOfBirth=@DateOfBirth,
	IdGender=@IdGender,
	IdAuthorityHuman=6,
	Address=@Address,
	Phone=@Phone,
	Email=@Email,
	UrlAvatarHuman=@UrlAvatarHuman,
	Forfeit=@Forfeit,
	PayFine=@PayFine,
	Compensation=@Compensation,
	Score=@Score,
	Note=@Note,
	CountDelete=@CountDelete
WHERE MS=@MS

	-- User Human (tài khoản người dùng)
CREATE PROC Update_UserHuman
	@IdHuman INT,
	@UserName NVARCHAR(100),
	@Password NVARCHAR(300),
	@DateInitPass DATETIME,
	@IdStatusChangePass INT, 	
	@DatePasswordChange DATETIME,
	@Note NVARCHAR(max),
	@CountDelete INT
AS
UPDATE UserHuman 
SET UserName=@UserName,
	Password=@Password,
	DateInitPass=@DateInitPass,
	IdStatusChangePass=@IdStatusChangePass,
	DatePasswordChange=@DatePasswordChange,
	Note=@Note,
	CountDelete=@CountDelete
WHERE IdHuman=@IdHuman

	-- User Staff (tài khoản nhân viên)
CREATE PROC Update_UserStaff
	@IdHuman INT,
	@UserName NVARCHAR(100),
	@Password NVARCHAR(300),
	@Note NVARCHAR(max),
	@ScoreInputBook INT,
	@ScoreOuputBook INT,
	@CountDelete INT
AS
UPDATE UserStaff 
SET UserName=@UserName,
	Password=@Password,
	IdAuthorityStaff=3,
	Note=@Note,
	ScoreInputBook=@ScoreInputBook,
	ScoreOuputBook=@ScoreOuputBook,
	CountDelete=@CountDelete
WHERE IdHuman=@IdHuman

	---- Author
CREATE PROC Update_Author
	@Id INT,
	@DisplayName NVARCHAR(max),
	@Address NVARCHAR(max),
	@Phone NVARCHAR(100),
	@Email NVARCHAR(200), 
	@Note NVARCHAR(max),
	@CountDelete INT
AS 
UPDATE Author 
SET DisplayName=@DisplayName,
	Address=@Address,
	Phone=@Phone,
	Email=@Email,
	Note=@Note,
	CountDelete=@CountDelete
WHERE Id=@Id

	-- Language Book
CREATE PROC Update_Language
	@Id INT,
	@DisplayName NVARCHAR(100),
	@Note NVARCHAR(max),
	@CountDelete INT
AS
UPDATE Language 
SET DisplayName=@DisplayName,
	Note=@Note,
	CountDelete=@CountDelete
WHERE Id=@Id

	---- Subject
CREATE PROC Update_BookSubject
	@Id INT,
	@DisplayName NVARCHAR(1000),
	@ScoreInputSubject INT,
	@ScoreOuputSubject INT,
	@Note NVARCHAR(max),
	@CountDelete INT 
AS 
UPDATE BookSubject 
SET DisplayName=@DisplayName,
	ScoreInputSubject=@ScoreInputSubject,
	ScoreOuputSubject=@ScoreOuputSubject,
	Note=@Note,
	CountDelete=@CountDelete
WHERE Id=@Id

--	Publisher
CREATE PROC Update_Publisher
	@Id INT,
	@DisplayName NVARCHAR(1000),
	@Address NVARCHAR(300),
	@Phone NVARCHAR(20),
	@Email NVARCHAR(200),
	@Note NVARCHAR(max),
	@Score FLOAT,
	@CountDelete INT
AS
UPDATE Publisher
SET DisplayName=@DisplayName,
	Address=@Address,
	Phone=@Phone,
	Email=@Email,
	Note=@Note,
	Score=@Score,
	CountDelete=@CountDelete
WHERE Id=@Id

	-- Book
CREATE PROC Update_Book
	@Id INT,
	@DisplayName NVARCHAR(1000),
	@BookPrice FLOAT,
	@BorrowingIdHuman INT,
	@BookSubject NVARCHAR(500),
	@Author NVARCHAR(500),
	@IdLanguage INT,
	@IdPublisher INT,
	@IdStatus INT,
	@IdStatusReturnBookToHuman INT,
	@LibraryDateBorrowed DATETIME,
	@LibraryDueDate DATETIME,
	@Color NVARCHAR(30),
	@DateReturnBookToHuman DATETIME,
	@UrlImageBook NVARCHAR(500),
	@Note NVARCHAR(max),
	@CountDelete INT
AS
UPDATE Book 
SET DisplayName=@DisplayName,
	BookPrice=@BookPrice,
	BorrowingIdHuman=@BorrowingIdHuman,
	BookSubject=@BookSubject,
	Author=@Author,
	IdLanguage=@IdLanguage,
	IdPublisher=@IdPublisher,
	IdStatus=@IdStatus,
	IdStatusReturnBookToHuman=@IdStatusReturnBookToHuman,
	LibraryDateBorrowed=@LibraryDateBorrowed,
	LibraryDueDate=@LibraryDueDate,
	Color=@Color,
	DateReturnBookToHuman=@DateReturnBookToHuman,
	UrlImageBook=@UrlImageBook,
	Note=@Note,
	CountDelete=@CountDelete
WHERE Id=@Id

	-- Borrow Book
CREATE PROC Update_BorrowBooks
	@IdHuman INT,
	@IdBook INT,
	@DateBorrowed DATETIME,
	@DueDate DATETIME,
	@IdStatus INT,
	@IdFined INT,
	@Color NVARCHAR(30),
	@ContractualFine FLOAT,
	@Note NVARCHAR(max),
	@CountDelete INT
AS
UPDATE BorrowBooks 
SET IdBook=@IdBook,
	DateBorrowed=@DateBorrowed,
	DueDate=@DueDate,
	IdStatus=@IdStatus,
	IdFined=@IdFined,
	Color=@Color,
	ContractualFine=@ContractualFine,
	Note=@Note,
	CountDelete=@CountDelete
WHERE IdHuman=@IdHuman

	-- Bill Of Human (hóa đơn in ra cho đối tác khi cho thư viện mượn sách)
CREATE PROC Update_BillBookOfHuman 
	@IdHuman INT,
	@IdStaff INT,
	@BorrowedDate DATETIME,
	@DateOfRepayment DATETIME,
	@Note NVARCHAR(max), 
	@IdStatusBill INT,
	@CountDelete INT
AS
UPDATE BillBookOfHuman 
SET IdStaff=@IdStaff,
	BorrowedDate=@BorrowedDate,
	DateOfRepayment=@DateOfRepayment,
	Note=@Note,
	IdStatusBill=@IdStatusBill,
	CountDelete=@CountDelete
WHERE IdHuman=@IdHuman

	-- Bill Of Customer (hóa đơn in ra cho khách hàng khi đến thư viện mượn sách)
CREATE PROC Update_BillBookOfCustomer  
	@IdHuman INT,
	@IdStaff INT,
	@BorrowedDate DATETIME,
	@DateOfRepayment DATETIME,
	@CashReceived FLOAT,
	@IdStatusBill INT,
	@Note NVARCHAR(max), 
	@CountDelete INT 
AS 
UPDATE BillBookOfCustomer
SET IdStaff=@IdStaff,
	BorrowedDate=@BorrowedDate,
	DateOfRepayment=@DateOfRepayment,
	CashReceived=@CashReceived,
	IdStatusBill=@IdStatusBill,
	Note=@Note,
	CountDelete=@CountDelete
WHERE IdHuman=@IdHuman

	-- Bill Customer Retun Book (hóa đơn in ra cho khách hàng khi trả lại sách cho thư viện)
CREATE PROC Update_BillCustomerReturnBookLibrary   
	@IdHuman INT,
	@IdStaff INT,
	@DateOfRepayment DATETIME,
	@AllTheDepositAmount FLOAT,
	@CashReceived FLOAT,
	@CustomerFined FLOAT,
	@AllFines FLOAT,
	@IdStatusBill INT,
	@Note NVARCHAR(max), 
	@CountDelete INT 
AS
UPDATE BillCustomerReturnBookLibrary 
SET IdStaff=@IdStaff,
	DateOfRepayment=@DateOfRepayment,
	AllTheDepositAmount=@AllTheDepositAmount,
	CashReceived=@CashReceived,
	CustomerFined=@CustomerFined,
	AllFines=@AllFines,
	IdStatusBill=@IdStatusBill,
	Note=@Note,
	CountDelete=@CountDelete 
WHERE IdHuman=@IdHuman

----	Bill Return Book Human (hóa đơn in ra cho đối tác khi đối tác nhận lại sách từ thư viện)
CREATE PROC Update_BillReturnBookHuman   
	@IdHuman INT,
	@IdStaff INT,
	@DateOfRepayment DATETIME,
	@IdStatusBill INT,
	@LibraryFined FLOAT,
	@Note NVARCHAR(max), 
	@CountDelete INT
AS
UPDATE BillReturnBookHuman 
SET IdStaff=@IdStaff,
	DateOfRepayment=@DateOfRepayment,
	IdStatusBill=@IdStatusBill,
	LibraryFined=@LibraryFined,
	Note=@Note,
	CountDelete=@CountDelete
WHERE @IdHuman=@IdHuman

--	History Create Time Table (lịch sử tạo thời gian biểu làm việc)	
CREATE PROC Update_HistoryCreateTimeTable
	@IdUserStaff INT ,
	@DisplayName NVARCHAR(2000),
	@DateCreate DATETIME
AS
UPDATE HistoryCreateTimeTable 
SET DisplayName=@DisplayName,
	DateCreate=@DateCreate
WHERE IdUserStaff=@IdUserStaff

Use Library_Management
Go

CREATE VIEW Human_vi AS
SELECT Human.Id, Human.MS, Human.DisplayName, Human.DateOfBirth, Gender.DisplayName as Gender, AuthorityHuman.DisplayName as Authority, Human.Address, Human.Phone, Human.Email, Human.UrlAvatarHuman as Avatar, Human.Forfeit, Human.PayFine, Human.Note
from Human join Gender on Gender.Id=Human.IdGender join AuthorityHuman on Human.IdAuthorityHuman=AuthorityHuman.Id
GO

CREATE VIEW BookBorrowed_vi AS SELECT Book.Id, Book.DisplayName, Book.BorrowingIdHuman, Book.BookSubject, Book.Author, 
Language.DisplayName as Language, Publisher.DisplayName as Publisher, Status.DisplayName as Status, Book.UrlImageBook, Book.Note 
from Book JOIN Publisher ON Book.IdPublisher=Publisher.Id join Language on Book.IdLanguage=Language.Id JOIN Status ON Book.IdStatus=Status.Id
WHERE Book.IdStatus = 1
GO

CREATE VIEW BookAvailable_vi AS SELECT Book.Id, Book.DisplayName, Book.BorrowingIdHuman, Book.BookSubject, Book.Author, 
Language.DisplayName as Language, Publisher.DisplayName as Publisher, Status.DisplayName as Status, Book.UrlImageBook, Book.Note 
from Book JOIN Publisher ON Book.IdPublisher=Publisher.Id join Language on Book.IdLanguage=Language.Id JOIN Status ON Book.IdStatus=Status.Id
WHERE Book.IdStatus = 2
GO

CREATE VIEW BookLiquidation_vi AS SELECT Book.Id, Book.DisplayName, Book.BorrowingIdHuman, Book.BookSubject, Book.Author, 
Language.DisplayName as Language, Publisher.DisplayName as Publisher, Status.DisplayName as Status, Book.UrlImageBook, Book.Note 
from Book JOIN Publisher ON Book.IdPublisher=Publisher.Id join Language on Book.IdLanguage=Language.Id JOIN Status ON Book.IdStatus=Status.Id
WHERE Book.IdStatus = 3
GO

CREATE VIEW BorrowBooks_vi AS SELECT BorrowBooks.Id, Human.DisplayName as Human, Book.DisplayName as Book, 
DateBorrowed, DueDate, Status.DisplayName as Status, Fined.DisplayName as Fine, BorrowBooks.Color, ContractualFine, BorrowBooks.Note 
FROM BorrowBooks JOIN Book ON BorrowBooks.IdBook=Book.Id JOIN Human ON BorrowBooks.IdHuman=Human.Id JOIN 
Status ON BorrowBooks.IdStatus=Status.Id JOIN Fined ON BorrowBooks.IdFined=Fined.Id
GO

