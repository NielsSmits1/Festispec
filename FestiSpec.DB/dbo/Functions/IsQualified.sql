CREATE FUNCTION [dbo].[IsQualified](@Inspection int, @Inspector int)
RETURNS int

BEGIN
DECLARE @result int
DECLARE @table table(am int)
INSERT INTO @table Select Certificaat_ID FROM Vereiste_certificaten WHERE Inspectienummer = @Inspection EXCEPT SELECT Certificaat_ID FROM Certificaat_inspecteur WHERE Inspecteur = @Inspector
SET @result = (
CASE 
WHEN (Select COUNT(am) FROM @table) = 0
	THEN 1
	ELSE 0
	END
)
RETURN @result
END
	
