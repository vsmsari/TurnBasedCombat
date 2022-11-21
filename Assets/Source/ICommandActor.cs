public interface ICommandActor
{
    // void AddCommands(bool _UseUndo, params CCommand_Base[] _Command);
    void AddCommandSet(CCommandSet_Base _CommandSet);
}