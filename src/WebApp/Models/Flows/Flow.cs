namespace WebApp.Models.Flows
{
    public record FlowNode(string Id, string Title, string? Subtitle = null, bool Disabled = false);

    public record FlowEdge(string FromId, string ToId);

}
