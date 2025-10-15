using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.Entities.Characters;
using DHCardHelper.Models.Entities.Relationships;
using DHCardHelper.Models.Entities.Users;
using DHCardHelper.Models.Types;
using DHCardHelper.Utilities.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DHCardHelper.Utilities.SeedDatabase
{
    public class DatabaseSeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public DatabaseSeeder(IUnitOfWork unitOfWork, IMyLogger logger, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;

        }
        public async Task SeedDatabaseAsync()
        {
            if (!await _unitOfWork.BackgroundCardTypeRepository.AnyAsync())
                await seedBackgroundCardTypes();

            if (!await _unitOfWork.CharacterClassRepository.AnyAsync())
                await seedCharacterClasses();

            if (!await _unitOfWork.DomainCardTypeRepository.AnyAsync())
                await seedDomainCardTypes();

            if (!await _unitOfWork.DomainRepository.AnyAsync())
                await seedDomains();

            if (!await _unitOfWork.CardRepository.AnyByTypeAsync<DomainCard>())
                await seedDomainCards();

            if (!await _unitOfWork.CardRepository.AnyByTypeAsync<SubclassCard>())
                await seedSubclassCards();

            if (!await _unitOfWork.CardRepository.AnyByTypeAsync<BackgroundCard>())
                await seedBackgroundCards();

            if (!await _unitOfWork.ClassToDomainRelRepository.AnyAsync())
                await seedClassDomainRels();

            if (!await _unitOfWork.CharacterSheetRepository.AnyAsync())
                await seedCharacterAndCardSheet();
        }

        private async Task seedBackgroundCardTypes()
        {
            await _unitOfWork.BackgroundCardTypeRepository.AddAsync(new BackgroundCardType { Name = "Origin" });
            await _unitOfWork.BackgroundCardTypeRepository.AddAsync(new BackgroundCardType { Name = "Society" });

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding BackgroundCardTypes - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding BackgroundCardTypes - {ex.Message}");
            }
        }

        private async Task seedCharacterClasses()
        {
            await _unitOfWork.CharacterClassRepository.AddAsync(new CharacterClass { Name = "Illuminator" });
            await _unitOfWork.CharacterClassRepository.AddAsync(new CharacterClass { Name = "Destroyer" });
            await _unitOfWork.CharacterClassRepository.AddAsync(new CharacterClass { Name = "Druid" });
            await _unitOfWork.CharacterClassRepository.AddAsync(new CharacterClass { Name = "Priest" });
            await _unitOfWork.CharacterClassRepository.AddAsync(new CharacterClass { Name = "Witch" });
            await _unitOfWork.CharacterClassRepository.AddAsync(new CharacterClass { Name = "Ranger" });
            await _unitOfWork.CharacterClassRepository.AddAsync(new CharacterClass { Name = "Warrior" });

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding CharacterClasses - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding CharacterClasses - {ex.Message}");
            }
        }

        private async Task seedDomainCardTypes()
        {
            await _unitOfWork.DomainCardTypeRepository.AddAsync(new DomainCardType { Name = "Spell"});
            await _unitOfWork.DomainCardTypeRepository.AddAsync(new DomainCardType { Name = "Ability"});

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding DomainCardTypes - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding DomainCardTypes - {ex.Message}");
            }
        }

        private async Task seedDomains()
        {
            await _unitOfWork.DomainRepository.AddAsync(new Domain { Name = "Fire", Color = "#d60618" });
            await _unitOfWork.DomainRepository.AddAsync(new Domain { Name = "Water", Color = "#1275e6" });
            await _unitOfWork.DomainRepository.AddAsync(new Domain { Name = "Air", Color = "#76d8db" });
            await _unitOfWork.DomainRepository.AddAsync(new Domain { Name = "Earth", Color = "#5fbf39" });
            await _unitOfWork.DomainRepository.AddAsync(new Domain { Name = "Light", Color = "#E0C043" });
            await _unitOfWork.DomainRepository.AddAsync(new Domain { Name = "Dark", Color = "#0f0f0e" });

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding Domains - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding Domains - {ex.Message}");
            }
        }

        private async Task seedDomainCards()
        {
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Flame Burst", Level = 1, RecallCost = 1, Feature = "Launches a small burst of fire that deals light damage to a single target.", DomainCardTypeId = 1, DomainId = 1});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Inferno Wave", Level = 4, RecallCost = 3, Feature = "Unleashes a sweeping wave of flame, burning all nearby enemies.", DomainCardTypeId = 1, DomainId = 1});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Ember Shield", Level = 2, RecallCost = 0, Feature = "Surrounds the caster with embers, reducing incoming damage for a short time.", DomainCardTypeId = 1, DomainId = 1});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Heat Adaptation", Level = 1, RecallCost = 1, Feature = "Increases resistance to fire damage by channeling inner heat.", DomainCardTypeId = 2, DomainId = 1});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Combustion Focus", Level = 3, RecallCost = 0, Feature = "Focuses fiery energy to increase the power of fire spells temporarily.", DomainCardTypeId = 2, DomainId = 1});

            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Frost Bolt", Level = 1, RecallCost = 1, Feature = "Fires a bolt of icy energy that slows the target.", DomainCardTypeId = 1, DomainId = 2});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Tidal Surge", Level = 4, RecallCost = 3, Feature = "Summons a surge of water to knock back enemies.", DomainCardTypeId = 1, DomainId = 2});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Healing Rain", Level = 2, RecallCost = 2, Feature = "Restores health to allies in a small area.", DomainCardTypeId = 1, DomainId = 2});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Ocean’s Endurance", Level = 3, RecallCost = 0, Feature = "Increases stamina regeneration by embracing the calm of the tides.", DomainCardTypeId = 2, DomainId = 2});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Flow State", Level = 1, RecallCost = 1, Feature = "Allows spells to cast faster for a short period.", DomainCardTypeId = 2, DomainId = 2});
            
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Gust", Level = 1, RecallCost = 1, Feature = "Creates a quick burst of wind that pushes back an enemy.", DomainCardTypeId = 1, DomainId = 3});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Storm Strike", Level = 4, RecallCost = 3, Feature = "Channels lightning into a single devastating attack.", DomainCardTypeId = 1, DomainId = 3});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Whirling Barrier", Level = 2, RecallCost = 2, Feature = "Forms a barrier of wind that deflects projectiles.", DomainCardTypeId = 1, DomainId = 3});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Quickstep", Level = 1, RecallCost = 0, Feature = "Increases movement speed temporarily", DomainCardTypeId = 2, DomainId = 3});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Wind Whisper", Level = 3, RecallCost = 1, Feature = "Grants a short-term evasion boost through heightened awareness.", DomainCardTypeId = 2, DomainId = 3});

            
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Stone Skin", Level = 1, RecallCost = 1, Feature = "Hardens the target’s skin, reducing damage taken.", DomainCardTypeId = 1, DomainId = 4});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Tremor", Level = 2, RecallCost = 0, Feature = "Creates a localized quake that knocks down enemies.", DomainCardTypeId = 1, DomainId = 4});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Thorn Growth", Level = 4, RecallCost = 3, Feature = "Summons thorny vines that damage enemies over time.", DomainCardTypeId = 1, DomainId = 4});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Rooted Resolve", Level = 1, RecallCost = 0, Feature = "Increases defense when standing still.", DomainCardTypeId = 2, DomainId = 4});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Mountain’s Will", Level = 3, RecallCost = 2, Feature = "Boosts endurance and reduces stagger chance.", DomainCardTypeId = 2, DomainId = 4});
            
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Radiant Beam", Level = 1, RecallCost = 1, Feature = "Fires a focused beam of pure light energy.", DomainCardTypeId = 1, DomainId = 5});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Holy Nova", Level = 1, RecallCost = 0, Feature = "Emits a burst of healing light, restoring allies and damaging dark-aligned foes.", DomainCardTypeId = 1, DomainId = 5});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Blinding Flash", Level = 3, RecallCost = 3, Feature = "Temporarily blinds enemies, reducing accuracy.", DomainCardTypeId = 1, DomainId = 5});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Purity of Heart", Level = 4, RecallCost = 2, Feature = "Increases resistance to corruption effects.", DomainCardTypeId = 2, DomainId = 5});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Beacon’s Blessing", Level = 2, RecallCost = 0, Feature = "Grants nearby allies minor healing over time.", DomainCardTypeId = 2, DomainId = 5});
            

            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Shadow Bolt", Level = 1, RecallCost = 0, Feature = "Launches a projectile of shadow energy that pierces defenses.", DomainCardTypeId = 1, DomainId = 6});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Abyssal Grasp", Level = 4, RecallCost = 3, Feature = "Summons dark tendrils to pull enemies toward the caster.", DomainCardTypeId = 1, DomainId = 6});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Dread Wave", Level = 2, RecallCost = 2, Feature = "Emits a pulse of fear that causes nearby foes to flee briefly.", DomainCardTypeId = 1, DomainId = 6});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Precise Shot", Level = 1, RecallCost = 1, Feature = "Enhances ranged accuracy with shadow focus.", DomainCardTypeId = 2, DomainId = 6});
            await _unitOfWork.CardRepository.AddAsync(new DomainCard { Title = "Essence Drain", Level = 3, RecallCost = 1, Feature = "Converts a portion of dealt damage into health.", DomainCardTypeId = 2, DomainId = 6});

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding DomainCards - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding DomainCards - {ex.Message}");
            }
        }

        private async Task seedSubclassCards()
        {
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Warden of Balance", Feature = "Restores health while balancing elemental forces.", MasteryType = MasteryType.Foundation, CharacterClassId = 3 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Warden of Balance", Feature = "Creates a zone where allies gain buffs and enemies are slowed.", MasteryType = MasteryType.Specialization, CharacterClassId = 3 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Warden of Balance", Feature = "Unites the power of earth and water, greatly enhancing resilience and healing.", MasteryType = MasteryType.Mastery, CharacterClassId = 3 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Warden of Nature", Feature = "Entangles a single enemy in roots.", MasteryType = MasteryType.Foundation, CharacterClassId = 3 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Warden of Nature", Feature = "Revives an ally from the brink of death using nature’s essence.", MasteryType = MasteryType.Specialization, CharacterClassId = 3 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Warden of Nature", Feature = "Transforms the caster into a guardian spirit, granting major healing and defense boosts.", MasteryType = MasteryType.Mastery, CharacterClassId = 3 });

            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Path of the Beastkeeper", Feature = "Summons a spirit wolf to aid in battle.", MasteryType = MasteryType.Foundation, CharacterClassId = 6 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Path of the Beastkeeper", Feature = "Increases damage when fighting alongside summoned beasts.", MasteryType = MasteryType.Specialization, CharacterClassId = 6 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Path of the Beastkeeper", Feature = "Commands all companions to perform a coordinated attack.", MasteryType = MasteryType.Mastery, CharacterClassId = 6 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Path of the Hunter", Feature = "Increases ranged accuracy.", MasteryType = MasteryType.Foundation, CharacterClassId = 6 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Path of the Hunter", Feature = "Marks an enemy to take increased damage from all sources.", MasteryType = MasteryType.Specialization, CharacterClassId = 6 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Path of the Hunter", Feature = "Fires a rain of arrows over a large area.", MasteryType = MasteryType.Mastery, CharacterClassId = 6 });

            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Keeper of the Faith", Feature = "Grants a minor healing aura.", MasteryType = MasteryType.Foundation, CharacterClassId = 4 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Keeper of the Faith", Feature = "Instantly heals an ally for a large amount.", MasteryType = MasteryType.Specialization, CharacterClassId = 4 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Keeper of the Faith", Feature = "Creates an invulnerable barrier for a short duration.", MasteryType = MasteryType.Mastery, CharacterClassId = 4 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Keeper of the Celestial Flame", Feature = "Burns enemies while healing nearby allies.", MasteryType = MasteryType.Foundation, CharacterClassId = 4 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Keeper of the Celestial Flame", Feature = "Revives fallen allies with partial health.", MasteryType = MasteryType.Specialization, CharacterClassId = 4 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Keeper of the Celestial Flame", Feature = "Summons radiant fire from the heavens to purge corruption.", MasteryType = MasteryType.Mastery, CharacterClassId = 4 });

            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Breaker of Chains", Feature = "Breaks enemy defenses with a powerful strike.", MasteryType = MasteryType.Foundation, CharacterClassId = 2 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Breaker of Chains", Feature = "Sends out a shockwave that damages and stuns enemies.", MasteryType = MasteryType.Specialization, CharacterClassId = 2 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Breaker of Chains", Feature = "Unleashes a massive explosion of dark fire energy.", MasteryType = MasteryType.Mastery, CharacterClassId = 2 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Breaker of Souls", Feature = "Damages the enemy’s spirit, reducing magic defense.", MasteryType = MasteryType.Foundation, CharacterClassId = 2 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Breaker of Souls", Feature = "Temporarily increases power at the cost of health.", MasteryType = MasteryType.Specialization, CharacterClassId = 2 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Breaker of Souls", Feature = "Channels dark energy to annihilate all enemies nearby.", MasteryType = MasteryType.Mastery, CharacterClassId = 2 });

            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Brewer of Poisons", Feature = "Inflicts a weak poison on contact.", MasteryType = MasteryType.Foundation, CharacterClassId = 5 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Brewer of Poisons", Feature = "Releases a toxic mist that damages all within.", MasteryType = MasteryType.Specialization, CharacterClassId = 5 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Brewer of Poisons", Feature = "Summons poisonous flora to spread deadly spores.", MasteryType = MasteryType.Mastery, CharacterClassId = 5 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Brewer of Potions", Feature = "Restores a small amount of health over time.", MasteryType = MasteryType.Foundation, CharacterClassId = 5 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Brewer of Potions", Feature = "Enhances spell potency temporarily.", MasteryType = MasteryType.Specialization, CharacterClassId = 5 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Brewer of Potions", Feature = "Greatly boosts all attributes for a limited time.", MasteryType = MasteryType.Mastery, CharacterClassId = 5 });

            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Way of the Hand", Feature = "A precise strike that deals high melee damage.", MasteryType = MasteryType.Foundation, CharacterClassId = 7 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Way of the Hand", Feature = "Slams the ground to stagger all nearby enemies.", MasteryType = MasteryType.Specialization, CharacterClassId = 7 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Way of the Hand", Feature = "Lifts and throws enemies, dealing massive damage.", MasteryType = MasteryType.Mastery, CharacterClassId = 7 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Way of the Righteous", Feature = "Emits a pulse of divine energy that damages nearby foes", MasteryType = MasteryType.Foundation, CharacterClassId = 7 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Way of the Righteous", Feature = "Reduces damage taken by all allies nearby.", MasteryType = MasteryType.Specialization, CharacterClassId = 7 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Way of the Righteous", Feature = "Calls down holy fire to smite all enemies in range.", MasteryType = MasteryType.Mastery, CharacterClassId = 7 });

            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Illusion of the Mind", Feature = "Creates an illusory clone to confuse enemies.", MasteryType = MasteryType.Foundation, CharacterClassId = 1 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Illusion of the Mind", Feature = "Deals psychic damage and disrupts enemy focus.", MasteryType = MasteryType.Specialization, CharacterClassId = 1 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Illusion of the Mind", Feature = "Bends reality, making the caster temporarily untargetable.", MasteryType = MasteryType.Mastery, CharacterClassId = 1 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Illusion of the Heart", Feature = "Creates blinding flashes to distract enemies.", MasteryType = MasteryType.Foundation, CharacterClassId = 1 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Illusion of the Heart", Feature = "Generates a shimmering field that distorts enemy aim.", MasteryType = MasteryType.Specialization, CharacterClassId = 1 });
            await _unitOfWork.CardRepository.AddAsync(new SubclassCard { Title = "Illusion of the Heart", Feature = "Channels pure light energy, healing allies and damaging shadows.", MasteryType = MasteryType.Mastery, CharacterClassId = 1 });

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding SubclassCards - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding SubclassCards - {ex.Message}");
            }
        }

        private async Task seedBackgroundCards()
        {
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Human", Desciption = "Adaptable and ambitious, humans are known for their ingenuity and resilience.", Feature = "Versatile Spirit – Gain a small bonus to any skill of your choice.", BackgroundTypeId = 1});
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Elf", Desciption = "Graceful beings attuned to nature and magic, elves live long lives of learning and balance.", Feature = "Arcane Affinity – Reduces mana cost of all spells slightly.", BackgroundTypeId = 1});
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Dwarf", Desciption = "Hardy miners and craftsmen from the deep mountains, dwarves value strength and endurance.", Feature = "Stone’s Endurance – Temporarily reduce incoming physical damage.", BackgroundTypeId = 1});
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Orc", Desciption = "Fierce and passionate, orcs channel primal energy in both battle and ritual.", Feature = "Battle Fury – Increases attack power briefly after taking damage.", BackgroundTypeId = 1});
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Fae", Desciption = "Mystical spirits of nature and illusion, the fae blend trickery with ethereal beauty.", Feature = "Glimmer Veil – Can briefly turn invisible or hard to target.", BackgroundTypeId = 1});

            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Noble", Desciption = "Born into privilege and power, nobles are trained in leadership and etiquette.", Feature = "Commanding Presence – Allies gain a morale boost when near you.", BackgroundTypeId = 2});
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Fisherman", Desciption = "Patient and steady, fishers know the rhythms of the sea and the value of persistence.", Feature = "Ocean’s Patience – Increases regeneration and stamina recovery.", BackgroundTypeId = 2});
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Scholar", Desciption = "Seekers of knowledge, scholars study ancient texts and magical theory.", Feature = "Insightful Mind – Gain bonus experience when discovering new lore.", BackgroundTypeId = 2});
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Mercenary", Desciption = "Hired blades with no allegiance but to coin, mercenaries survive through grit and instinct.", Feature = "Combat Ready – Start each battle with a small damage boost.", BackgroundTypeId = 2});
            await _unitOfWork.CardRepository.AddAsync(new BackgroundCard { Title = "Nomad", Desciption = "Wanderers who live by their adaptability and intuition, finding home wherever they roam.", Feature = "Wayfarer’s Instinct – Move faster and take less fatigue while traveling.", BackgroundTypeId = 2});
            
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding BackgroundCards - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding BackgroundCards - {ex.Message}");
            }
        }

        private async Task seedClassDomainRels()
        {
            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 1, DomainId = 1 });
            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 1, DomainId = 5 });

            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 2, DomainId = 1 });
            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 2, DomainId = 6 });

            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 3, DomainId = 2 });
            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 3, DomainId = 4 });

            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 4, DomainId = 2 });
            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 4, DomainId = 5 });

            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 5, DomainId = 2 });
            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 5, DomainId = 6 });

            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 6, DomainId = 3 });
            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 6, DomainId = 4 });

            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 7, DomainId = 1 });
            await _unitOfWork.ClassToDomainRelRepository.AddAsync(new ClassToDomainRel { ClassId = 7, DomainId = 4 });

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding ClassToDomainRel - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding ClassToDomainRel - {ex.Message}");
            }
        }

        private async Task seedCharacterAndCardSheet()
        {
            var player = _userManager.Users.FirstOrDefault(u => u.Email == "player@player.com");

            if (player == null)
            {
                _logger.Error("Player not found! Cannot create characters!");
                return;
            }

            var characterYanric = new CharacterSheet { Name = "Yanric Neryl", User = player };
            var characterZargothrax = new CharacterSheet { Name = "Zargothrax", User = player };

            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterYanric, CardId = 2, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterYanric, CardId = 5, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterYanric, CardId = 26, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterYanric, CardId = 29, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterYanric, CardId = 49, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterYanric, CardId = 50, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterYanric, CardId = 74, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterYanric, CardId = 81, InLimit = false, InLoadout = false });

            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterZargothrax, CardId = 7, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterZargothrax, CardId = 9, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterZargothrax, CardId = 22, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterZargothrax, CardId = 25, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterZargothrax, CardId = 46, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterZargothrax, CardId = 47, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterZargothrax, CardId = 73, InLimit = false, InLoadout = false });
            await _unitOfWork.CardSheetRepository.AddAsync( new CardSheet { CharacterSheet = characterZargothrax, CardId = 80, InLimit = false, InLoadout = false });

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Saving failed when seeding CharacterAndCardSheets - {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occured when seeding CharacterAndCardSheets - {ex.Message}");
            }
        }

    }
}
