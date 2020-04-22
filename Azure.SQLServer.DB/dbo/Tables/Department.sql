CREATE TABLE [dbo].[Department] (
    [DeptID]   INT          IDENTITY (1, 1) NOT NULL,
    [DeptName] VARCHAR (50) NULL,
    [isActive] BIT          DEFAULT ((0)) NULL
);

