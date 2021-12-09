using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class Detector
    {

        [JsonPropertyName("minNumFindings")]
        public int MinNumFindings { get; set; } = 1;

        [JsonPropertyName("minConfidence")]
        public string MinConfidence { get; set; } = ConfidenceEnum.POSSIBLE.ToString();

        [JsonPropertyName("detectorUUID")]
        public string DetectorUUID { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("detectorType")]
        public string DetectorType { get; set; }

        [JsonPropertyName("nightfallDetector")]
        public string NightfallDetector { get; set; }

        [JsonPropertyName("regex")]
        public Regex Regex { get; set; }

        [JsonPropertyName("wordList")]
        public WordList WordList { get; set; }

        [JsonPropertyName("contextRules")]
        public List<ContextRule> ContextRules { get; set; }

        [JsonPropertyName("exclusionRules")]
        public List<ExclusionRule> ExclusionRules { get; set; }

        [JsonPropertyName("redactionConfig")]
        public RedactionConfig RedactionConfig { get; set; }

        public Detector()
        {
        }

        public Detector(string detectorUuid,int minNumFindings=1,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE )
        {
            DetectorUUID = detectorUuid;
            MinNumFindings = minNumFindings;
            MinConfidence = minConfidence.ToString();
        }
        private Detector(string displayName, ConfidenceEnum minConfidence, int minNumFindings)
        {
            DisplayName = displayName;
            MinNumFindings = minNumFindings;
            MinConfidence = minConfidence.ToString();
        }
        public Detector(DetectorGlossary nightfallDetector,string displayName=null,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE,int minNumFindings=1) : this(displayName, minConfidence, minNumFindings)
        {
            DetectorType = Models.DetectorType.NIGHTFALL_DETECTOR.ToString();
            NightfallDetector = nightfallDetector.ToString();
        }
        
        public Detector(Regex regex,string displayName=null,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE,int minNumFindings=1) : this(displayName, minConfidence, minNumFindings)
        {
            DetectorType = Models.DetectorType.REGEX.ToString();
            Regex = regex;
        }
        
        public Detector(WordList wordList,string displayName=null,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE,int minNumFindings=1) : this(displayName, minConfidence, minNumFindings)
        {
            DetectorType = Models.DetectorType.WORD_LIST.ToString();
            WordList = wordList;
        }


        public void SetConfidence(ConfidenceEnum confidence)
        {
            MinConfidence = confidence.ToString();
        }

        public Detector WithExclusionRules(params ExclusionRule[] exclusionRules)
        {
            ExclusionRules ??= new List<ExclusionRule>();
            ExclusionRules.AddRange(exclusionRules);
            return this;
        }
        public Detector WithRegexExclusionRule(MatchType matchType=MatchType.PARTIAL,string pattern = null, bool isCaseSensitive = default)
        {
            ExclusionRules ??= new List<ExclusionRule>();
            ExclusionRules.Add(new ExclusionRule(matchType,new Regex(pattern,isCaseSensitive)));
            return this;
        }
        public Detector WithWordListRule(MatchType matchType=MatchType.PARTIAL,ICollection<string> words = null, bool isCaseSensitive = default)
        {
            ExclusionRules ??= new List<ExclusionRule>();
            ExclusionRules.Add(new ExclusionRule(matchType,new WordList(words,isCaseSensitive)));
            return this;
        }
        public Detector WithContextRules(params ContextRule[] contextRules)
        {
            ContextRules ??= new List<ContextRule>();
            ContextRules.AddRange(contextRules);
            return this;
        }
        
        public Detector WithContextRule(string pattern,bool isCaseSensitive=default, int windowBefore = 10, int windowAfter = 10,
            ConfidenceEnum confidenceAdjustment = ConfidenceEnum.VERY_LIKELY)
        {
            ContextRules ??= new List<ContextRule>();
            ContextRules.Add(ContextRule.BuildContextRule(pattern,isCaseSensitive,windowAfter,windowAfter,confidenceAdjustment));
            return this;
        }
        
        public Detector WithRedactionConfig(RedactionConfig redactionConfig)
        {
            RedactionConfig = redactionConfig;
            return this;
        }

        public Detector WithCryptoRedactionConfig(string secretKey)
        {
            RedactionConfig=new RedactionConfig(new CryptoConfig(secretKey));
            return this;
        }
        public Detector WithInfoTypeSubstitutionRedactionConfig()
        {
            RedactionConfig=new RedactionConfig(new InfoTypeSubstitutionConfig());
            return this;
        }
        public Detector WithMaskRedactionConfig(string maskingChar, List<string> charsToIgnore, int numCharsToLeaveUnmasked,
            bool maskLeftToRight)
        {
            RedactionConfig=new RedactionConfig(new MaskConfig(maskingChar,charsToIgnore,numCharsToLeaveUnmasked,maskLeftToRight));
            return this;
        }
        public Detector WithSubstitutionRedactionConfig(string substitutionPhrase)
        {
            RedactionConfig=new RedactionConfig(new SubstitutionConfig(substitutionPhrase));
            return this;
        }
    }
}