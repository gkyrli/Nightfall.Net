using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using static Nightfall.Net.Models.DetectorType;

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
        public ICollection<string> Values { get; set; }

        [JsonPropertyName("isCaseSensitive")]
        public bool IsCaseSensitive { get; set; }

        public WordList()
        {
        }

        public WordList(ICollection<string> values = null, bool isCaseSensitive = default)
        {
            Values = values;
            IsCaseSensitive = isCaseSensitive;
        }
        public WordList(string[] values = null, bool isCaseSensitive = default)
        {
            Values = values.ToList();
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

        public static ContextRule BuildContextRule(string pattern,bool caseSensitive=default, int windowBefore = 10, int windowAfter = 10,
            ConfidenceEnum confidenceAdjustment = ConfidenceEnum.VERY_LIKELY)
        {
            return new ContextRule(new Regex(pattern, caseSensitive), new Proximity(windowBefore, windowAfter),
                new ConfidenceAdjustment() {FixedConfidence = confidenceAdjustment.ToString()});
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


    /// <summary>
    /// Provides a configuration to let the api know how to mask the finding.
    /// </summary>
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

    /// <summary>
    /// Provide a string that is going to be used to replace the finding
    /// </summary>
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

        public CryptoConfig(string publicKey)
        {
            PublicKey = publicKey;
        }
    }

    /// <summary>
    /// An object that configures how any detected findings should be redacted when returned to the client. When this
    /// configuration is provided as part of a request, exactly one of the four types of redaction should be set.
    /// </summary>
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

        public RedactionConfig(InfoTypeSubstitutionConfig infoTypeSubstitutionConfig)
        {
            InfoTypeSubstitutionConfig = infoTypeSubstitutionConfig;
        }

        public RedactionConfig(SubstitutionConfig substitutionConfig)
        {
            SubstitutionConfig = substitutionConfig;
        }

        public RedactionConfig(MaskConfig maskConfig)
        {
            MaskConfig = maskConfig;
        }

        public RedactionConfig(CryptoConfig cryptoConfig)
        {
            CryptoConfig = cryptoConfig;
        }
    }

    public class Detector
    {
        public string DetectorUuid { get; }

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
            DetectorUuid = detectorUuid;
            MinNumFindings = minNumFindings;
            MinConfidence = minConfidence.ToString();
        }
        private Detector(string displayName, ConfidenceEnum minConfidence, int minNumFindings)
        {
            DisplayName = displayName;
            MinNumFindings = minNumFindings;
            MinConfidence = minConfidence.ToString();
        }
        public Detector(string nightfallDetector,string displayName=null,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE,int minNumFindings=1) : this(displayName, minConfidence, minNumFindings)
        {
            DetectorType = NIGHTFALL_DETECTOR.ToString();
            NightfallDetector = nightfallDetector;
        }
        
        public Detector(Regex regex,string displayName=null,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE,int minNumFindings=1) : this(displayName, minConfidence, minNumFindings)
        {
            DetectorType = REGEX.ToString();
            Regex = regex;
        }
        
        public Detector(WordList wordList,string displayName=null,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE,int minNumFindings=1) : this(displayName, minConfidence, minNumFindings)
        {
            DetectorType = WORD_LIST.ToString();
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

    public class DetectionRule
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("logicalOp")]
        public string LogicalOp { get; }

        [JsonPropertyName("detectors")]
        public ICollection<Detector> Detectors { get; }

        private DetectionRule(string name, LogicalOperation logicalOp, ICollection<Detector> detectors = null)
        {
            Name = name;
            LogicalOp = logicalOp.ToString();
            Detectors = detectors ?? new HashSet<Detector>();
        }

        public static DetectionRule GetANYDetectionRule(string name, ICollection<Detector> detectors = null)
        {
            return new DetectionRule(name, LogicalOperation.ANY, detectors);
        }

        public static DetectionRule GetALLDetectionRule(string name)
        {
            return new DetectionRule(name, LogicalOperation.ALL);
        }

        public void AddUUidDetector(string uuid,int minNumFindings=1,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE )
        {
            Detectors.Add(new Detector(){DetectorUUID = uuid,MinNumFindings = minNumFindings,MinConfidence = minConfidence.ToString()});
        }
        public void AddDetector(Detector detector)
        {
            Detectors.Add(detector);
        }

        // public DetectionRule WithNightfallDetector(string nightfallDetector)
        // {
        //     Detectors.Add(new Detector(nightfallDetector));
        //     return this;
        // }
        //
        // public DetectionRule WithWords(ICollection<string> wordList,bool caseSensitive=default)
        // {
        //     Detectors.Add(new Detector(new WordList(wordList,caseSensitive)));
        //     return this;
        // }
        //
        // public DetectionRule WithWords(WordList wordList)
        // {
        //     Detectors.Add(new Detector(wordList));
        //     return this;
        // }
        // public DetectionRule WithRegex(Regex regex)
        // {
        //     Detectors.Add(new Detector(regex));
        //     return this;
        // }
        // public DetectionRule WithRegex(string pattern,bool isCaseSensitive=default)
        // {
        //     Detectors.Add(new Detector(new Regex(pattern,isCaseSensitive)));
        //     return this;
        // }
    }

    public abstract class PolicyBase
    {
        [JsonPropertyName("webhookURL")]
        public string WebhookURL { get; internal set; }

        [JsonPropertyName("detectionRuleUUIDs")]
        public HashSet<string> DetectionRuleUUIDs { get; set; }

        [JsonPropertyName("detectionRules")]
        public List<DetectionRule> DetectionRules { get; set; }

        public void AddDetectionRule(params DetectionRule[] rule)
        {
            DetectionRules ??= new List<DetectionRule>();
            DetectionRules.AddRange(rule);
        }

        public void AddDetectionRuleUUIDs(params string[] uuids)
        {
            DetectionRuleUUIDs ??= new HashSet<string>();
            DetectionRuleUUIDs.UnionWith(uuids);
        }
    }

    public class Config : PolicyBase
    {
        public Config(int contextBytes) => ContextBytes = contextBytes;

        [JsonPropertyName("contextBytes")]
        public int ContextBytes { get; set; }
    }

    public abstract class ScanBase
    {
        [JsonPropertyName("config")]
        public virtual PolicyBase Config { get; set; }

        public void AddDetectionRules(params DetectionRule[] rule) => Config.AddDetectionRule(rule);
        public void AddDetectionRuleUUids(params string[] uuids) => Config.AddDetectionRuleUUIDs(uuids);

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