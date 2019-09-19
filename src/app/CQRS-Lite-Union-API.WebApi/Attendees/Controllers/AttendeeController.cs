﻿using AutoMapper;
using CQRS_Lite_Union_API.Application.Attendees.Commands;
using CQRS_Lite_Union_API.Application.Attendees.Result;
using CQRS_Lite_Union_API.WebApi.Attendees.Models.RegisterAttendee;
using CQRS_Lite_Union_API.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_Lite_Union_API.WebApi.Attendees.Controllers
{
    [Route("/api/attendee")]
    public class AttendeeController : BaseController
    {
        public AttendeeController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {   

        }

        [HttpPost]
        public async Task<IActionResult> RegisterAttendee(RegisterAttendeeRequest request) // command 
        {
            var command = Mapper.Map<RegisterAttendeeCommand>(request);
            var response = await Mediator.Send(command);
            return BuildHttpResponse<RegisterAttendeeResult, RegisterAttendeeResponse>(response);
        }
    }
}
