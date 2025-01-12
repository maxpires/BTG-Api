using BTG.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Api.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));
        }

        [HttpGet("ValorTotal/{codigoPedido}")]
        public async Task<IActionResult> GetValorTotalDoPedido(int codigoPedido)
        {
            try
            {
                var ret = await _pedidoService.GetValorTotalDoPedido(codigoPedido);

                if (ret == null)
                    return NotFound($"Pedido com código { codigoPedido } não encontrado");

                return Ok(ret);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("QuantidadePedidos/{codigoCliente}")]
        public async Task<IActionResult> GetQuantidadePedidosPorCliente(int codigoCliente)
        {
            try
            {
                var ret = await _pedidoService.GetQuantidadePedidosPorCliente(codigoCliente);

                if (ret == null)
                    return NotFound($"Pedido do cliente { codigoCliente } não encontrado");
                
                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pedidos/{codigoCliente}")]
        public async Task<IActionResult> GetListaPedidosPorCliente(int codigoCliente)
        {
            try
            {
                var ret = await _pedidoService.GetListaPedidosPorCliente(codigoCliente);

                if (ret.Count() == 0)
                    return NotFound($"Nenhum pedido encontrado para o cliente com código {codigoCliente}.");
                
                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
