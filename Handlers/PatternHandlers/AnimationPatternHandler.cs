using Handlers.Abstractions.PatternHandlerSettings;

namespace Handlers.PatternHandlers;

internal class AnimationPatternHandler : PatternHandler<AnimationPatternHandlerSetting>
{
    private int _frameNumber;
    private LinkedList<AnimationInfo> _animationInfos = [];
    private LinkedListNode<AnimationInfo>? _playableAnimationInfo = null;

    public AnimationPatternHandler(AnimationPatternHandlerSetting setting) : base(setting)
    {
        _animationInfos = new LinkedList<AnimationInfo>(_setting.AnimationInfos);
        _frameNumber = 1;
        _playableAnimationInfo = _animationInfos.First;
    }

    private string GetNextFrame()
    {
        if (_playableAnimationInfo == null)
            return string.Empty;

        var val = _playableAnimationInfo.Value;
        var result = val.Text;

        _frameNumber++;
        if (_frameNumber > val.FrameCount)
        {
            _frameNumber = 1;
            _playableAnimationInfo = _playableAnimationInfo.Next;
            if (_playableAnimationInfo == null)
            {
                _playableAnimationInfo = _animationInfos.First;
            }
        }

        return result;
    }

    protected override Task<string> GetInnerResult()
    {
        return Task.FromResult(GetNextFrame());
    }
}
