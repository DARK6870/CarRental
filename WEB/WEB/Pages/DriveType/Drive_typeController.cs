using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Pages.DriveType.Commands;
using WEB.Pages.DriveType.Queryes;

namespace WEB.Pages.DriveType
{
    [Authorize(Roles = "Admin")]
    public class Drive_typeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public Drive_typeController(IAppCache cache, IMediator mediator)
        {
            _mediator = mediator;
            _cache = cache;
        }



        public async Task<IActionResult> IndexDriveType()
        {
            var drive_type = await _cache.GetOrAddAsync("drive_type_data", async () =>
            {
                var result = await _mediator.Send(new GetAllDriveTypesQuery());
                return result;
            });
            if (drive_type != null) return View(drive_type);
            else return Problem("ERROR");
        }

        public IActionResult CreateDriveType()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDriveType(Drive_type drive_type)
        {
            try
            {
                await _mediator.Send(new AddDriveTypeCommand(drive_type));
                _cache.Remove("drive_type_data");

                return RedirectToAction("IndexDriveType");
            }
            catch
            {
                return View(drive_type);
            }
        }



        public async Task<IActionResult> EditDriveType(byte id)
        {
            try
            {
                var drive_type = await _mediator.Send(new GetDriveTypeByIdQuery(id));

                if (drive_type == null)
                {
                    return NotFound();
                }
                return View(drive_type);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDriveType(byte id, Drive_type drive_type)
        {
            if (id != drive_type.drive_id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new UpdateDriveTypeCommand(drive_type));
                _cache.Remove("drive_type_data");
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
            }
            return RedirectToAction("IndexDriveType");
        }



        public async Task<IActionResult> DeleteDriveType(byte id)
        {
            try
            {
                var drive_type = await _mediator.Send(new GetDriveTypeByIdQuery(id));

                if (drive_type == null)
                {
                    return NotFound();
                }

                return View(drive_type);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost, ActionName("DeleteDriveType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (await _mediator.Send(new GetAllDriveTypesQuery()) == null)
            {
                return Problem("Entity set 'AppDBContext.Drive_type'  is null.");
            }
            var drive_type = await _mediator.Send(new GetDriveTypeByIdQuery(id));

            if (drive_type != null)
            {
                await _mediator.Send(new DeleteDriveTypeCommand(drive_type));
                _cache.Remove("drive_type_data");
            }
            return RedirectToAction("IndexDriveType");
        }
    }
}
