using Microsoft.EntityFrameworkCore;
using System.Globalization;

public class InviteService : IInviteService
{
    private readonly DataBase _context;

    public InviteService(DataBase context)
    {
        _context = context;
    }

    public async Task<bool> CreateInvite(CreateInviteModel body)
    {
        var user = await _context.Users.Where(u => body.UserId.Contains(u.Id)).ToListAsync();
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var _event = await _context.Events.FirstOrDefaultAsync(u => u.Id == body.EventId);
        if (_event == null)
        {
            throw new NotEventException();
        }

        var _invite = new Invite
        {
            Name = body.Name,
            Description = string.IsNullOrEmpty(body.Description) ? "Default description" : body.Description,
            EventId = body.EventId,
            UserId = user.Select(u => u.Id).ToList(),
            Date = body.Date,
        };

        await _context.Invites.AddAsync(_invite);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteInvite(int InviteId)
    {
        var invite = await _context.Invites.FirstOrDefaultAsync(u => u.Id == InviteId);
        if (invite == null)
        {
            throw new Exception("Invite not found");
        }

        _context.Invites.Remove(invite);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteInvitesByEvent(int eventId)
    {
        var invites = await _context.Invites.Where(i => i.EventId == eventId).ToListAsync();
        if (invites == null || invites.Count == 0)
        {
            throw new Exception("No invites found for the specified event");
        }

        _context.Invites.RemoveRange(invites);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Invite(InviteModel body)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == body.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var invite = await _context.Invites.FirstOrDefaultAsync(u => u.Id == body.InviteId);
        if (invite == null)
        {
            throw new Exception("Invite not found");
        }

        invite.UserId.Add(user.Id);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Edit(InviteEditModel body)
    {
        var invite = await _context.Invites.FirstOrDefaultAsync(u => u.Id == body.Id);
        if (invite == null)
        {
            throw new NotEventException();
        }

        invite.Name = body.Name ?? invite.Name;
        invite.Date = body.Date ?? invite.Date;
        invite.Description = body.Description ?? invite.Description;
        invite.UserId = body.UserId ?? invite.UserId;
        invite.EventId = body.EventId != null? body.EventId : invite.EventId;
        await _context.SaveChangesAsync();
        return true;
    }
}

