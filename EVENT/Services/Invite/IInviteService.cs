
    public interface IInviteService
    {
    public Task<bool> CreateInvite(CreateInviteModel body);

    public Task<bool> DeleteInvite(int InviteId);

    public Task<bool> DeleteInvitesByEvent(int eventId);

    public Task<bool> Invite(InviteModel body);

    public Task<bool> Edit(InviteEditModel body);
}

