SELECT 
	d.Title,
	p.FullName AS Product,
	pv.Release AS [Version],
	d.HtmlLink,
	d.PdfLink,
	d.WordLink,
	up.LatestTopicsUpdated,
	d.ShortDescription,
	up.Timestamp AS [Update],
	a.Alias AS Author,
	a.Email,
	cat.Name,
	t.FullName	
FROM DocumentUpdate AS up
LEFT JOIN Document AS d ON d.Id = up.DocumentId
LEFT JOIN DocumentAuthor AS da ON d.Id = da.DocumentId
LEFT JOIN Author AS a ON a.Id = da.AuthorId
LEFT JOIN DocumentCatalog AS dc ON dc.DocumentId = d.Id
LEFT JOIN ClientCatalog AS cat ON cat.Id = dc.CatalogId
LEFT JOIN DocumentType AS t ON t.Id = d.DocumentTypeId
LEFT JOIN ProductVersion AS pv ON pv.Id = d.ProductVersionId
LEFT JOIN Product AS p ON p.Id = pv.ProductId
WHERE up.IsPublished = 1
ORDER BY up.Timestamp DESC, Product, Version DESC