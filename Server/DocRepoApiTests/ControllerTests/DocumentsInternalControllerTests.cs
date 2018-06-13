using AutoMapper;
using DocRepoApi.Controllers;
using DocRepoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ControllerTests
{
    public class DocumentsInternalControllerTests
    {
        /// <summary>
        /// Mapper test context.
        /// </summary>
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        private DocumentDtoInternal GetTestDocumentDtoFromContext(int id)
        {
            var dbTestContext = DbTestContext.GenerateContextWithData();          
            return _mapper.Map<DocumentDtoInternal>(dbTestContext.Documents.Single(d => d.Id == id));
        }
        

        #region GET: api/v1/DocumentsInternal Tests
        [Fact(DisplayName = "GetDocuments() should return a list of the first 20 Documents")]
        public void GetDocumentsReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = controller.GetDocuments();
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(20));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }
        [Fact(DisplayName = "GetDocuments(50) should return a list of the first 50 Documents")]
        public void GetDocuments50ReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = controller.GetDocuments(50);
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(50));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }
        [Fact(DisplayName = "GetDocuments(20,2) should return a list of the second 20 Documents")]
        public void GetDocuments20_2ReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = controller.GetDocuments(20, 2);
                DocumentDtoInternal d44 = GetTestDocumentDtoFromContext(44);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(20));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 44);
                Assert.True(d.Equals(d44));
                Assert.True(d.Equals(d44, true));

            }
        }

        [Fact(DisplayName = "GetDocuments() should return NotFound if context is empty")]
        public void GetDocumentsEmptyContextNotFound()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = controller.GetDocuments();

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetDocuments(50,5) should return NotFound if page is empty")]
        public void GetDocuments50_5NotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = controller.GetDocuments(50, 5);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetDocuments() should return Bad Request if Model with Error")]
        public void GetDocumentsBadModel()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                var result = controller.GetDocuments();

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region GET: api/v1/DocumentsInternal/4 Tests
        [Fact(DisplayName = "GetDocument(4) should return the document with an ID of 4")]
        public async void GetDocumentByIdReturnsCorrectDocument()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocument(4);
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.NotNull(resultValue);
                Assert.IsAssignableFrom<DocumentDtoInternal>(resultValue);
                DocumentDtoInternal d = (DocumentDtoInternal)resultValue;

                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }

        [Fact(DisplayName = "GetDocument(999) should return NotFound")]
        public async void GetDocumentWithIncorrectId()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocument(999);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetDocument(4) should return Bad Request if Model with Error")]
        public async void GetDocumentBadModel()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                var result = await controller.GetDocument(4);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion

        #region GET: api/v1/DocumentsInternal/ByProduct/1 Tests
        [Fact(DisplayName = "GetDocumentsByProduct(1) should return the first 20 Documents")]
        public async void GetDocumentsByProductReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProduct(1);
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(20));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }
        [Fact(DisplayName = "GetDocumentsByProduct(1,10) should return the first 10 Documents")]
        public async void GetDocumentsByProduct1_10ReturnsFiveDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProduct(1,10);
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(10));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }
        [Fact(DisplayName = "GetDocumentsByProduct(1,10,2) should return the second 10 Documents")]
        public async void GetDocumentsByProduct1_10_2ReturnsFiveDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProduct(1, 10, 2);
                DocumentDtoInternal d24 = GetTestDocumentDtoFromContext(24);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(10));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 24);
                Assert.True(d.Equals(d24));
                Assert.True(d.Equals(d24, true));

            }
        }

        [Fact(DisplayName = "GetDocumentsByProduct(999) should return NotFound if context is empty")]
        public async void GetDocumentsByProductWithIncorrectProductId()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProduct(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetDocumentsByProduct(1,50,2) should return NotFound if page is empty")]
        public async void GetDocumentsByProduct1_50_2NotFound()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProduct(1, 50, 2);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetDocumentsByProduct(1) should return Bad Request if Model with Error")]
        public async void GetDocumentsByProductBadModel()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                var result = await controller.GetDocumentsByProduct(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region GET: api/v1/DocumentsInternal/ByProductVersion/1 Tests
        [Fact(DisplayName = "GetDocumentsByProductVersion(1) should return a list of the first 20 Documents")]
        public async void GetDocumentsByProductVersionReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProductVersion(1);
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(5));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }
        [Fact(DisplayName = "GetDocumentsByProductVersion(1,5) should return a list of the first 5 Documents")]
        public async void GetDocumentsByProductVersion1_5ReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProductVersion(1, 5);
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(5));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }
        [Fact(DisplayName = "GetDocumentsByProductVersion(1,2,2) should return a list of the second 5 Documents")]
        public async void GetDocumentsByProductVersion1_2_2ReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProductVersion(1, 2, 2);
                DocumentDtoInternal d6 = GetTestDocumentDtoFromContext(6);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(2));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 6);
                Assert.True(d.Equals(d6));
                Assert.True(d.Equals(d6, true));

            }
        }

        [Fact(DisplayName = "GetDocumentsByProductVersion(999) should return NotFound if context is empty")]
        public async void GetDocumentsByProductVersionWithIncorrectProductId()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProductVersion(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetDocumentsByProductVersion(1,10,2) should return NotFound if page is empty")]
        public async void GetDocumentsByProductVersion1_10_2EmptyPage()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByProductVersion(1, 10, 2);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetDocumentsByProductVersion(1) should return Bad Request if Model with Error")]
        public async void GetDocumentsByProductVersionBadModel()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                var result = await controller.GetDocumentsByProductVersion(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region GET: api/v1/DocumentsInternal/ByDocType/1 Tests
        [Fact(DisplayName = "GetDocumentsByType(1) should return a list of all Documents")]
        public async void GetDocumentsByTypeReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByType(1);
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }

        [Fact(DisplayName = "GetDocumentsByType(999) should return NotFound if context is empty")]
        public async void GetDocumentsByTypeWithIncorrectProductId()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsByType(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetDocumentsByType(1) should return Bad Request if Model with Error")]
        public async void GetDocumentsByTypeBadModel()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                var result = await controller.GetDocumentsByType(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region api/v1/DocumentsInternal/Search Tests
        [Fact(DisplayName = "GetDocumentsFromSearch(test) should return a list of the first 20 Documents")]
        public async void GetDocumentsFromSearchReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("test");
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(20));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(test, 20, 2) should return a list of the second 20 Documents")]
        public async void GetDocumentsFromSearch20_2ReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("test", 20, 2);
                DocumentDtoInternal d44 = GetTestDocumentDtoFromContext(44);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(20));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 44);
                Assert.True(d.Equals(d44));
                Assert.True(d.Equals(d44, true));

            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(test, 50) should return a list of the first 50 Documents")]
        public async void GetDocumentsFromSearch50ReturnsListOfDocuments()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("test", 50);
                DocumentDtoInternal d4 = GetTestDocumentDtoFromContext(4);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(50));

                DocumentDtoInternal d = (DocumentDtoInternal)resultValueList.Single(r => r.Id == 4);
                Assert.True(d.Equals(d4));
                Assert.True(d.Equals(d4, true));

            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(Document 1) should return 6 Documents with matching title")]
        public async void GetDocumentsFromSearchByTitleReturnsMatches()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("Document 1");                

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(6));                

            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(Test Document #1) should return 6 Documents with matching title")]
        public async void GetDocumentsFromSearchByDescriptionReturnsMatches()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("Test Document #1");

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(6));

            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(Document 2 - Version) should return 11 Documents with short description")]
        public async void GetDocumentsFromSearchByLatestUpdateReturnsMatches()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("- Version");

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(20));

            }
        }

        // Fuzzy Search

        [Fact(DisplayName = "GetDocumentsFromSearch(Test Document, 150, 1, true) should return 50 Documents")]
        public async void GetDocumentsFromSearchExactSearchReturnsMatches()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("Test Document", 150, 1, true);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(50));

            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(Document Test, 150, 1, true) should return a Not Fund Error")]
        public async void GetDocumentsFromSearchExactSearchReturnsNoMatches()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("Document Test", 150, 1, true);
                Assert.IsType<NotFoundResult>(result);

                //Assert.NotNull(result);
                //var okObjectResult = Assert.IsType<OkObjectResult>(result);
                //var resultValue = okObjectResult.Value;
                //Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                //Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                //IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                //Assert.True(resultValueList.Count().Equals(50));

            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(Document Test, 150, 1, false) should return 50 Documents")]
        public async void GetDocumentsFromSearchFuzzySearchReturnsMatches()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("Document Test", 150, 1, false);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentDtoInternal>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentDtoInternal>)resultValue);

                IEnumerable<DocumentDtoInternal> resultValueList = (IEnumerable<DocumentDtoInternal>)resultValue;
                Assert.True(resultValueList.Count().Equals(50));

            }
        }
        

        [Fact(DisplayName = "GetDocumentsFromSearch(mihailo) should a NotFound error")]
        public async void GetDocumentsFromSearchNoDocumentsFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("mihailo");               

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(test, 50, 3) should return a NotFound error")]
        public async void GetDocumentsFromSearch50_3NoDocumentsFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("test", 50, 3);               

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(mihailo,100,1,false) should a NotFound error")]
        public async void GetDocumentsFromSearchFuzzySearchNoDocumentsFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                var result = await controller.GetDocumentsFromSearch("mihailo", 100, 1, false);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact(DisplayName = "GetDocumentsFromSearch(test) should return Bad Request if Model with Error")]
        public async void GetDocumentsFromSearchBadModel()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                var result = await controller.GetDocumentsFromSearch("test");

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        #endregion

        #region GET: api/v1/DocumentsInternal/1/Updates Tests
        [Fact(DisplayName = "GetDocumentUpdates(4) should return all document updates")]
        public async void GetDocumentUpdatesReturnsListOfUpdates()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentsInternalController(context, _mapper))
            {                
                var result = await controller.GetDocumentUpdates(4);                

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentUpdateDto>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentUpdateDto>)resultValue);

                IEnumerable<DocumentUpdateDto> resultValueList = (IEnumerable<DocumentUpdateDto>)resultValue;
                Assert.True(resultValueList.Count().Equals(10));                

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
    }
}
