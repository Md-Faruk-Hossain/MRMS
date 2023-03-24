﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRMS_Blazor.Shared;
using MRMS_Blazor.Shared.DemandSection;

namespace MRMS_Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DemandsController : ControllerBase
    {

        private IGlobalRepository _globalRepo;
        private IGenericRepository<Demand> _demandRepo;

        public DemandsController(IGlobalRepository globalRepo)
        {
            this._globalRepo= globalRepo;
            this._demandRepo = _globalRepo.GetRepository<Demand>();
        }

        //Get Data
        [HttpGet]
        public IEnumerable<Demand> GetDemands()
        {
            return _demandRepo.GetAll();
        }

        //Get Demand By Id
        [HttpGet("{id}")]
        public ActionResult<Demand> GetDemandById(int id)
        {
            Demand demand = _demandRepo.Get(id);

            if (demand is null)
            {
                return NotFound();
            }
            return demand;

        }

        [HttpPost]
        public IActionResult PostDemand(Demand demand)
        {
            _demandRepo.Insert(demand);
            _globalRepo.Save();
            return Ok(demand);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDemand(int id, Demand demand)
        {
            if (id !=demand.DemandId  )
            {
                return NotFound();
            }           
            _demandRepo.Update(demand);
            _globalRepo.Save();

            return Ok(demand);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDemand(int id)
        {   
            Demand demand= _demandRepo.Get(id);
            if(demand == null)
            {
                return NotFound();
            }
            _demandRepo.Delete(demand);
            _globalRepo.Save();
            return Ok(demand);
        }
    }
}
