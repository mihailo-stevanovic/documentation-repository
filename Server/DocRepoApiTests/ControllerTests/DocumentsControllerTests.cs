using AutoMapper;
using DocRepoApi.Controllers;
using DocRepoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocRepoApiTests.ControllerTests
{
    public class DocumentsControllerTests
    {
        /// <summary>
        /// Mapper test context.
        /// </summary>
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        private DocumentDto GetTestDocumentDtoFromContext(int id)
        {
            var dbTestContext = DbTestContext.GenerateContextWithData();
            var document = dbTestContext.Documents                
                .Single(d => d.Id == id);

            return _mapper.Map<DocumentDto>(document);
                
        }

        private DocumentDto GetTestDocumentDto(int id)
        {
            return new DocumentDto
            {
                Id = id,
                AitId = id,
                ParentDocumentId = 1,
                ProductVersionId = 5,
                DocumentTypeId = 2,
                DocumentAuthorIds = new List<int> { 1, 2 },               
                DocumentCatalogIds = new List<int> { 1, 2 },                  
                IsFitForClients = (id % 2 == 0),

                Title = $"Document {id}",
                ShortDescription = $"This is Test Document #{id}",
                HtmlLink = $"TestDocuments/{id}/HTML/index.htm",
                PdfLink = $"TestDocuments/{id}/PDF/TestDocument_{id}.pdf",
                WordLink = $"TestDocuments/{id}/DOC/TestDocument_{id}.doc",

                IsPublished = true,
                LatestTopicsUpdated = "This is the first version of the document."
                
            };
        }

        private DocumentUpdateDto GetTestDocumentUpdateDto(int documentId, int version, bool ommitId = false)
        {
            return new DocumentUpdateDto
            {
                Id = ommitId ? 0 : documentId * version,
                DocumentId = documentId,
                LatestTopicsUpdated = $"Document {documentId} - Version {version}",
                IsPublished = (documentId % 2 == 0),
                Timestamp = DateTime.Today.AddDays(version)
            };
        }

        #region Documents Tests
        #region Test GET Methods
        [Fact(DisplayName = "GetDocuments() should return a list of all Documents")]
        public void GetDocumentsReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = controller.GetDocuments();
                DocumentDto d3 = GetTestDocumentDtoFromContext(3);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDto>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDto>)resultValue);

                IEnumerable<DocumentDto> resultValueList = (IEnumerable<DocumentDto>)resultValue;

                DocumentDto d = (DocumentDto)resultValueList.Single(r => r.Id == 3);
                Assert.True(d.Equals(d3));
                Assert.True(d.Equals(d3, true));

            }
        }

        [Fact(DisplayName = "GetDocuments() should return NotFound if context is empty")]
        public void GetDocumentsEmptyContextNotFound()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = controller.GetDocuments();

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetDocument(id) should return the Document")]
        public async void GetDocumentByIdReturnsSingleDocument()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = await controller.GetDocument(3);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;

                DocumentDto a3 = GetTestDocumentDtoFromContext(3);

                Assert.NotNull(resultValue);
                Assert.IsType<DocumentDto>(resultValue);
                DocumentDto a = (DocumentDto)resultValue;
                Assert.True(a.Equals(a3));
                Assert.True(a.Equals(a3, true));
            }
        }

        [Fact(DisplayName = "GetDocument(wrongId) should return NotFound")]
        public async void GetDocumentWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = await controller.GetDocument(999);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetDocument(id) with ModelStateError should return BadRequest")]
        public async void GetDocumentModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.GetDocument(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion

        #region Test POST Methods
        // POST Methods
        [Fact(DisplayName = "PostDocument(Document) should create a new Document")]
        public async void PostDocumentCorrectDataCreatesDocument()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                DocumentDto a101 = GetTestDocumentDto(101);

                var result = await controller.PostDocument(a101);

                Assert.NotNull(result);
                var resultValue = Assert.IsType<CreatedAtActionResult>(result);


            }
        }

        [Fact(DisplayName = "PostDocument(Document) with ModelStateError should return BadRequest")]
        public async void PostDocumentModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                DocumentDto a101 = GetTestDocumentDto(101);

                var result = await controller.PostDocument(a101);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }        

        [Fact(DisplayName = "PostMultipleDocuments(DocumentList) should create multiple new Documents")]
        public async void PostMultipleDocumentsCorrectDataCreatesDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                List<DocumentDto> DocumentList = Enumerable.Range(101, 10).Select(i => GetTestDocumentDto(i)).ToList();

                var result = await controller.PostDocuments(DocumentList);

                Assert.NotNull(result);
                Assert.IsType<CreatedAtActionResult>(result);
            }
        }

        [Fact(DisplayName = "PostMultipleDocuments(DocumentList) with ModelStateError should return BadRequest")]
        public async void PostMultipleDocumentsModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                List<DocumentDto> DocumentList = Enumerable.Range(101, 10).Select(i => GetTestDocumentDto(i)).ToList();

                var result = await controller.PostDocuments(DocumentList);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region Test DELETE Methods

        // DELETE Methods
        [Fact(DisplayName = "DeleteDocument(id) should remove the Document from context")]
        public async void DeleteDocumentIdDeletesDocument()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = await controller.DeleteDocument(3);
                DocumentDto a3 = GetTestDocumentDtoFromContext(3);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.Equal(a3, resultValue);

            }
        }

        [Fact(DisplayName = "DeleteDocument(wrongId) should return NotFound")]
        public async void DeleteDocumentWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = await controller.DeleteDocument(999);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteDocument(id) with ModelStateError should return BadRequest")]
        public async void DeleteDocumentModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.DeleteDocument(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion
        #endregion

        #region Document Updates Tests
        #region GET

        [Fact(DisplayName = "GetDocumentUpdates(4) should return all Document Updates related to the Document")]
        public async void GetDocumentUpdatesReturnsListOfUpdates()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentUpdates(4);
                DocumentUpdateDto upTest = GetTestDocumentUpdateDto(4, 10);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentUpdateDto>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentUpdateDto>)resultValue);

                IEnumerable<DocumentUpdateDto> resultValueList = (IEnumerable<DocumentUpdateDto>)resultValue;
                Assert.True(resultValueList.Count().Equals(10));

                DocumentUpdateDto up = resultValueList.Single(u => u.Id == 40);

                Assert.True(upTest.Equals(up));
                Assert.True(upTest.Equals(up, true));

            }
        }

        [Fact(DisplayName = "GetDocumentUpdates(1) should return NotFound if document is not published")]
        public async void GetDocumentUpdatestWithUnpublishedDocumentId()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentUpdates(1);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetDocumentUpdates(999) should return NotFound if context is empty")]
        public async void GetDocumentUpdatestWithIncorrectDocumentId()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentUpdates(999);

                Assert.IsType<NotFoundResult>(result);

            }
        }



        [Fact(DisplayName = "GetDocumentUpdates(1) should return Bad Request if Model with Error")]
        public async void GetDocumentUpdatesBadModel()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                var result = await controller.GetDocumentUpdates(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion

        #region POST
        [Fact(DisplayName = "PostDocumentUpdate(4, DocumentUpdate) should create a new DocumentUpdate")]
        public async void PostDocumentUpdateCorrectDataCreatesDocumentUpdate()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                DocumentUpdateDto a101 = GetTestDocumentUpdateDto(4, 11, true);

                var result = await controller.PostDocumentUpdate(4, a101);

                Assert.NotNull(result);
                var resultValue = Assert.IsType<CreatedAtActionResult>(result);
                
            }
        }

        [Fact(DisplayName = "PostDocumentUpdate(4, DocumentUpdate) with ModelStateError should return BadRequest")]
        public async void PostDocumentUpdateModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                DocumentUpdateDto a101 = GetTestDocumentUpdateDto(4, 101);

                var result = await controller.PostDocumentUpdate(4, a101);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        [Fact(DisplayName = "PostDocumentUpdate(4, DocumentUpdate) should return BadRequest if Doc ID is 999")]
        public async void PostDocumentUpdateInvalidDocIdReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {                
                DocumentUpdateDto a101 = GetTestDocumentUpdateDto(999, 101);

                var result = await controller.PostDocumentUpdate(4, a101);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        [Fact(DisplayName = "PostDocumentUpdate(999, DocumentUpdate) should return BadRequest")]
        public async void PostDocumentUpdateInexistantDocIdReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                DocumentUpdateDto a101 = GetTestDocumentUpdateDto(999, 101);

                var result = await controller.PostDocumentUpdate(999, a101);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region DELETE
        [Fact(DisplayName = "DeleteDocumentUpdate(id) should remove the DocumentUpdate from context")]
        public async void DeleteDocumentUpdateIdDeletesDocumentUpdate()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = await controller.DeleteDocumentUpdate(1, 1);
                DocumentUpdateDto a3 = GetTestDocumentUpdateDto(1, 1);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.Equal(a3, resultValue);

            }
        }

        [Fact(DisplayName = "DeleteDocumentUpdate(1,999) should return NotFound")]
        public async void DeleteDocumentUpdateWithIncorrectUpIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = await controller.DeleteDocumentUpdate(1, 999);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteDocumentUpdate(4,1) should return NotFound")]
        public async void DeleteDocumentUpdateWithIncorrectDocIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = await controller.DeleteDocumentUpdate(4, 1);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteDocumentUpdate(999,1) should return NotFound")]
        public async void DeleteDocumentUpdateWithInvalidDocIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                var result = await controller.DeleteDocumentUpdate(999, 1);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteDocumentUpdate(id) with ModelStateError should return BadRequest")]
        public async void DeleteDocumentUpdateModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.DeleteDocumentUpdate(1, 1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion
        #endregion
    }
}
