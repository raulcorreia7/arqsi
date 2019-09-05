using AutoMapper;
using Closify.Controllers;
using FluentAssertions;
using Closify.Repository.Interfaces;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Closify.Models.DTO;

namespace Closify.Tests.Categoria
{
    public class CategoriaControllerUnitTests
    {
        [Fact]
        public async Task Categorias_Get_All()
        {
            var MockRepo = new Mock<ICategoriaRepository>();

            var MockMapper = new Mock<IMapper>();

            var controller = new CategoriasController(MockRepo.Object, MockMapper.Object);

            var result = await controller.GetAllCategorias();

            Assert.IsType<OkObjectResult>(result);

            //var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            //var categorias = okResult.Value.Should().BeAssignableTo<List<Models.Categoria>>().Subject;

            //categorias.Count().Should().Be(10);

            //var model = Assert.IsAssignableFrom<IEnumerable<>>(result.)
        }

        //[Fact]
        //public async Task Categoria_Get_Single()
        //{
        //    var MockRepo = new Mock<ICategoriaRepository>();

        //    var MockMapper = new Mock<IMapper>();

        //    Models.Categoria m = new Models.Categoria { CategoriaID = 1, Nome = "Armário" };

        //    //CategoriaDTO dto = MockMapper.Object.Map<Models.Categoria, CategoriaDTO>(m);

        //    CategoriaDTO dto = new CategoriaDTO { CategoriaDTOID = 1, Nome = "Armário" };

        //    MockRepo.Setup(x => x.Create(m));
            
        //    var controller = new CategoriasController(MockRepo.Object, MockMapper.Object);

        //    //var MockController = new Mock<CategoriasController>(MockRepo.Object, MockMapper.Object);

        //    //await MockController.Object.Create(dto);

        //    await controller.Create(dto);

        //    //var result = await MockController.Object.Details(1);

        //    var result = await controller.Details(1);

        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        //    //var categoria = okResult.Value.Should().BeAssignableTo<CategoriaDTO>().Subject;

        //    //categoria.CategoriaDTOID.Should().Be(1);
        //}
    }
}
