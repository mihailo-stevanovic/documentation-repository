using AutoMapper;
using DocRepoApi.Controllers;
using DocRepoApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DocRepoApiTests.ControllerTests
{
    public class DocumentTypesControllerTests
    {
        private IMapper _mapper = MapperTestContext.GenerateTestMapperContext();

        #region Generate Test Document Type Data
        /// <summary>
        /// Create a single Document Type for testing.
        /// </summary>
        /// <param name="i">ID of the document type.</param>
        /// <returns></returns>
        private DocumentTypeDto GetTestDocumentTypeDto(int i)
        {
            DocumentCategory docCat;
            if (i > 8)
            {
                docCat = DocumentCategory.FunctionalDocumentation;
            }
            else
            {
                docCat = (DocumentCategory)(i < 5 ? i : i - 4) - 1;
            }
            
            return new DocumentTypeDto
            {
                Id = i,
                FullName = $"Doc Type{i}",
                ShortName = $"DT{i}",
                DocumentCategory = docCat
            };
        }
        #endregion

        #region Test GET METHODS

        // Get Methods
        [Fact(DisplayName = "GetDocumentTypes() should return a list of all DocumentTypes")]
        public void GetDocumentTypesReturnsListOfDocumentTypes()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                var result = controller.GetDocumentTypes();
                DocumentTypeDto p3 = GetTestDocumentTypeDto(3);

                Assert.NotNull(result);

                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.IsAssignableFrom<IEnumerable<DocumentTypeDto>>(resultValue);
                Assert.NotEmpty((IEnumerable<DocumentTypeDto>)resultValue);

                IEnumerable<DocumentTypeDto> resultValueList = (IEnumerable<DocumentTypeDto>)resultValue;

                Assert.Equal(10, resultValueList.Count());
                DocumentTypeDto p = (DocumentTypeDto)resultValueList.Where(r => r.Id == 3).FirstOrDefault();
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));                               
            }
        }

        [Fact(DisplayName = "GetDocumentTypes() should return NotFound if context is empty")]
        public void GetDocumentTypesEmptyContextNotFound()
        {
            using (var context = DbTestContext.GenerateEmptyContext())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                var result = controller.GetDocumentTypes();

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "GetDocumentType(id) should return the DocumentType")]
        public async void GetDocumentTypeByIdReturnsSingleDocumentType()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                var result = await controller.GetDocumentType(3);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;

                DocumentTypeDto p3 = GetTestDocumentTypeDto(3);

                Assert.NotNull(resultValue);
                Assert.IsType<DocumentTypeDto>(resultValue);
                DocumentTypeDto p = (DocumentTypeDto)resultValue;
                Assert.True(p.Equals(p3));
                Assert.True(p.Equals(p3, true));
            }
        }


        [Fact(DisplayName = "GetDocumentType(wrongId) should return NotFound")]
        public async void GetDocumentTypeWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                var result = await controller.GetDocumentType(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact(DisplayName = "GetDocumentType(id) with ModelStateError should return BadRequest")]
        public async void GetDocumentTypeModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.GetDocumentType(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion

        #region Test POST Methods
        // POST Methods
        [Fact(DisplayName = "PostDocumentType(DocumentType) should create a new DocumentType")]
        public async void PostDocumentTypeCorrectDataCreatesDocumentType()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                DocumentTypeDto p11 = GetTestDocumentTypeDto(11);

                var result = await controller.PostDocumentType(p11);

                Assert.NotNull(result);
                var resultValue = Assert.IsType<CreatedAtActionResult>(result);


            }
        }

        [Fact(DisplayName = "PostDocumentType(DocumentType) with ModelStateError should return BadRequest")]
        public async void PostDocumentTypeModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");
                DocumentTypeDto p11 = GetTestDocumentTypeDto(11);

                var result = await controller.PostDocumentType(p11);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion

        #region Test DELETE Methods
        // DELETE Methods
        [Fact(DisplayName = "DeleteDocumentType(id) should remove the DocumentType from context")]
        public async void DeleteDocumentTypeIdDeletesDocumentType()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                var result = await controller.DeleteDocumentType(3);
                DocumentTypeDto p3 = GetTestDocumentTypeDto(3);

                Assert.NotNull(result);
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okObjectResult.Value;
                Assert.Equal(p3, resultValue);

            }
        }

        [Fact(DisplayName = "DeleteDocumentType(wrongId) should return NotFound")]
        public async void DeleteDocumentTypeWithIncorrectIdReturnsNotFound()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                var result = await controller.DeleteDocumentType(99);

                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact(DisplayName = "DeleteDocumentType(id) with ModelStateError should return BadRequest")]
        public async void DeleteDocumentTypeModelStateErrorReturnsBadRequest()
        {
            using (var context = DbTestContext.GenerateContextWithData())
            using (var controller = new DocumentTypesController(context, _mapper))
            {
                controller.ModelState.AddModelError("an error", "some error");

                var result = await controller.DeleteDocumentType(1);

                Assert.IsType<BadRequestObjectResult>(result);

            }
        }
        #endregion
    }
}
