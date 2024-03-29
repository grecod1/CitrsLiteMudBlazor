﻿using CitrsLite.Data.Models;
using Microsoft.AspNetCore.Components;

namespace CitrsLite.Shared
{
    public partial class ParticipantTable
    {
        private string? search;

        [Parameter]
        public IEnumerable<Participant>? Participants { get; set; }

        [Parameter]
        public bool Loading { get; set; } = false;
        

        private bool filterFunc1(Participant participant) => filterFunc(participant, search);

        
        private bool filterFunc(Participant participant, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return true;
            }

            if (participant.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (participant.Type.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (participant.City.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (participant.PhoneNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (participant.State.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

    }
}
