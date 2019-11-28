CREATE PROCEDURE test3 @id nvarchar(30), @path nvarchar(100), @type nvarchar(20), @new_val nvarchar(100) 
AS
  EXEC('update [dbo].[Table] set field1.modify(''replace value of (/' + @path + '/text())[1] with "' + @new_val + '" cast as ' + @type +' ?'') where Id=' + @id)
