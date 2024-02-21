Create database HttpAppDb;

use HttpAppDb;

Create table Courses(
[id] int primary key identity,
[Name] nvarchar(100),
[Price] money,
[Discription] nvarchar(100),
)
