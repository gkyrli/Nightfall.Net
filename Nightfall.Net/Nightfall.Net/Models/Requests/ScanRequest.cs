using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class Regex
    {
        [JsonPropertyName("pattern")]
        public string Pattern { get; set; }

        [JsonPropertyName("isCaseSensitive")]
        public bool IsCaseSensitive { get; set; }

        public Regex()
        {
        }

        public Regex(string pattern = null, bool isCaseSensitive = default)
        {
            Pattern = pattern;
            IsCaseSensitive = isCaseSensitive;
        }
    }

    public class WordList
    {
        [JsonPropertyName("values")]
        public List<string> Values { get; set; }

        [JsonPropertyName("isCaseSensitive")]
        public bool IsCaseSensitive { get; set; }

        public WordList()
        {
        }

        public WordList(List<string> values = null, bool isCaseSensitive = default)
        {
            Values = values;
            IsCaseSensitive = isCaseSensitive;
        }
    }

    public class Proximity
    {
        [JsonPropertyName("windowBefore")]
        public int WindowBefore { get; set; }

        [JsonPropertyName("windowAfter")]
        public int WindowAfter { get; set; }

        public Proximity()
        {
        }

        public Proximity(int windowBefore = 0, int windowAfter = 0)
        {
            WindowBefore = windowBefore;
            WindowAfter = windowAfter;
        }
    }

    public class ConfidenceAdjustment
    {
        [JsonPropertyName("fixedConfidence")]
        public string FixedConfidence { get; set; }
    }

    public class ContextRule
    {
        [JsonPropertyName("regex")]
        public Regex Regex { get; set; }

        [JsonPropertyName("proximity")]
        public Proximity Proximity { get; set; }

        [JsonPropertyName("confidenceAdjustment")]
        public ConfidenceAdjustment ConfidenceAdjustment { get; set; }

        public ContextRule()
        {
        }

        public ContextRule(Regex regex = null, Proximity proximity = null,
            ConfidenceAdjustment confidenceAdjustment = null)
        {
            Regex = regex;
            Proximity = proximity;
            ConfidenceAdjustment = confidenceAdjustment;
        }
    }

    public class ExclusionRule
    {
        [JsonPropertyName("matchType")]
        public string MatchType { get; set; }

        [JsonPropertyName("exclusionType")]
        public string ExclusionType { get; set; }

        [JsonPropertyName("regex")]
        public Regex Regex { get; set; }

        [JsonPropertyName("wordList")]
        public WordList WordList { get; set; }

        public ExclusionRule()
        {
        }

        public ExclusionRule(MatchType matchType, Regex regex)
        {
            MatchType = matchType.ToString();
            Regex = regex;
            ExclusionType = "REGEX";
        }

        public ExclusionRule(MatchType matchType, WordList wordList)
        {
            MatchType = matchType.ToString();
            WordList = wordList;
            ExclusionType = "WORD_LIST";
        }
    }
    public enum MatchType
    {
        PARTIAL,
        FULL
    }
    public class MaskConfig
    {
        [JsonPropertyName("maskingChar")]
        public string MaskingChar { get; set; }

        [JsonPropertyName("charsToIgnore")]
        public List<string> CharsToIgnore { get; set; }

        [JsonPropertyName("numCharsToLeaveUnmasked")]
        public int NumCharsToLeaveUnmasked { get; set; }

        [JsonPropertyName("maskLeftToRight")]
        public bool MaskLeftToRight { get; set; }

        public MaskConfig()
        {
        }

        public MaskConfig(string maskingChar, List<string> charsToIgnore, int numCharsToLeaveUnmasked,
            bool maskLeftToRight)
        {
            MaskingChar = maskingChar;
            CharsToIgnore = charsToIgnore;
            NumCharsToLeaveUnmasked = numCharsToLeaveUnmasked;
            MaskLeftToRight = maskLeftToRight;
        }
    }

    public class InfoTypeSubstitutionConfig
    {
    }

    public class SubstitutionConfig
    {
        [JsonPropertyName("substitutionPhrase")]
        public string SubstitutionPhrase { get; set; }

        public SubstitutionConfig()
        {
        }

        public SubstitutionConfig(string substitutionPhrase = null)
        {
            SubstitutionPhrase = substitutionPhrase;
        }
    }

    public class CryptoConfig
    {
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }
    }

    public class RedactionConfig
    {
        [JsonPropertyName("maskConfig")]
        public MaskConfig MaskConfig { get; set; }

        [JsonPropertyName("infoTypeSubstitutionConfig")]
        public InfoTypeSubstitutionConfig InfoTypeSubstitutionConfig { get; set; }

        [JsonPropertyName("substitutionConfig")]
        public SubstitutionConfig SubstitutionConfig { get; set; }

        [JsonPropertyName("cryptoConfig")]
        public CryptoConfig CryptoConfig { get; set; }

        [JsonPropertyName("removeFinding")]
        public bool RemoveFinding { get; set; }

        public RedactionConfig()
        {
        }

        public RedactionConfig(MaskConfig maskConfig = null,
            InfoTypeSubstitutionConfig infoTypeSubstitutionConfig = null, SubstitutionConfig substitutionConfig = null,
            CryptoConfig cryptoConfig = null, bool removeFinding = default)
        {
            MaskConfig = maskConfig;
            InfoTypeSubstitutionConfig = infoTypeSubstitutionConfig;
            SubstitutionConfig = substitutionConfig;
            CryptoConfig = cryptoConfig;
            RemoveFinding = removeFinding;
        }
    }

    public class Detector
    {
        [JsonPropertyName("minNumFindings")]
        public int MinNumFindings { get; set; }

        [JsonPropertyName("minConfidence")]
        public string MinConfidence { get; set; }

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

        public Detector(int minNumFindings = 1, ConfidenceEnum minConfidence = ConfidenceEnum.POSSIBLE, string detectorUuid = null,
            string displayName = null, string detectorType = null, string nightfallDetector = null, Regex regex = null,
            WordList wordList = null, List<ContextRule> contextRules = null, List<ExclusionRule> exclusionRules = null,
            RedactionConfig redactionConfig = null)
        {
            MinNumFindings = minNumFindings;
            MinConfidence = minConfidence.ToString();
            DetectorUUID = detectorUuid;
            DisplayName = displayName;
            DetectorType = detectorType;
            NightfallDetector = nightfallDetector;
            Regex = regex;
            WordList = wordList;
            ContextRules = contextRules;
            ExclusionRules = exclusionRules;
            RedactionConfig = redactionConfig;
        }

        public void SetConfidence(ConfidenceEnum confidence)
        {
            MinConfidence = confidence.ToString();
        }

        public enum ConfidenceEnum
        {
            VERY_UNLIKELY,
            UNLIKELY,
            POSSIBLE,
            LIKELY,
            VERY_LIKELY
        }
    }

    public class DetectionRule
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("logicalOp")]
        public string LogicalOp { get; }

        [JsonPropertyName("detectors")]
        public ICollection<Detector> Detectors { get; }

        private DetectionRule(string name, string logicalOp, ICollection<Detector> detectors = null)
        {
            Name = name;
            LogicalOp = logicalOp;
            Detectors = detectors ?? new HashSet<Detector>();
        }

        public static DetectionRule GetAnyDetectionRule(string name, ICollection<Detector> detectors = null)
        {
            return new DetectionRule(name, "ANY", detectors);
        }

        public static DetectionRule GetAndDetectionRule(string name)
        {
            return new DetectionRule(name, "ALL");
        }

        public void AddDetector(Detector detector)
        {
            Detectors.Add(detector);
        }
    }

    public abstract class PolicyBase
    {
        [JsonPropertyName("webhookURL")]
        public string WebhookURL { get;internal set; }
        [JsonPropertyName("detectionRuleUUIDs")]
        public HashSet<string> DetectionRuleUUIDs { get; set; }

        [JsonPropertyName("detectionRules")]
        public List<DetectionRule> DetectionRules { get; set; }

        public void AddDetectionRule(DetectionRule rule)
        {
            DetectionRules ??= new List<DetectionRule>();
            DetectionRules.Add(rule);
        }

        public void AddDetectionRuleUUIDs(params string[] uuids)
        {
            DetectionRuleUUIDs ??= new HashSet<string>();
            DetectionRuleUUIDs.UnionWith(uuids);
        }
    }

    public class Config : PolicyBase
    {
        public Config(int contextBytes)
        {
            ContextBytes = contextBytes;
        }

        [JsonPropertyName("contextBytes")]
        public int ContextBytes { get; set; }
    }

    public abstract class ScanBase
    {
        [JsonPropertyName("config")]
        public virtual PolicyBase Config { get; set; }

        public void AddDetectionRule(DetectionRule rule)
        {
            Config.AddDetectionRule(rule);
        }

        public void AddDetectionRuleUUids(params string[] uuids)
        {
            Config.AddDetectionRuleUUIDs(uuids);
        }

        protected ScanBase(PolicyBase config)
        {
            Config = config;
        }
    }

    public class ScanRequestConfig : ScanBase
    {
        [JsonPropertyName("payload")]
        public List<string> Payload { get; set; } = new List<string>();

        /// <param name="contextBytes">Maximum value is 40</param>
        public ScanRequestConfig(int? contextBytes = null) : base(new Config(contextBytes ?? 0))
        {
        }

        public void AddPayload(params string[] value)
        {
            Payload.AddRange(value);
        }
    }
}