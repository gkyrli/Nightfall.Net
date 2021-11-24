namespace Nightfall.Net.Models
{
    public enum ConfidenceEnum
    {
        VERY_UNLIKELY,
        UNLIKELY,
        POSSIBLE,
        LIKELY,
        VERY_LIKELY
    }

    public enum MatchType
    {
        PARTIAL,
        FULL
    }

    public enum LogicalOperation
    {
        ANY,
        ALL
    }

    public enum DetectorType
    {
        NIGHTFALL_DETECTOR,
        REGEX,
        WORD_LIST
    }
    
}