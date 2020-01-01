CREATE PROCEDURE [dbo].[Getqualifiedinspector](@id INT) 
AS 
    DECLARE @Date DATE = (SELECT startdate 
       FROM   inspectie 
       WHERE  inspectienummer = @id); 

    SELECT DISTINCT i.id 
    FROM   inspecteur AS i 
           JOIN naw_inspecteur AS ni 
             ON ni.id = i.naw 
           JOIN beschikbaarheid AS b 
             ON ni.id = b.inspecteur_id 
           JOIN certificaat_inspecteur ci 
             ON ci.inspecteur = i.id 
    WHERE  b.datum = @Date 
           AND i.id NOT IN(SELECT i2.id 
                           FROM   inspectie AS ins 
                                  INNER JOIN inspecteurs_inspectie AS ii 
                                          ON ii.inspectienummer = 
                                             ins.inspectienummer 
                                  INNER JOIN inspecteur AS i2 
                                          ON i2.id = ii.inspecteur_id 
                           WHERE  ins.startdate = @Date) 
           AND dbo.Isqualified(@id, i.id) = 1 
								 
