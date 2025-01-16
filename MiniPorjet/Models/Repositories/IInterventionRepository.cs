namespace MiniPorjet.Models.Repositories
{
    public interface IInterventionRepository
    {
        IEnumerable<Intervention> GetAll();
        Intervention GetById(int id);

        IEnumerable<Intervention> GetInterventionsByStatus(string status);


        void Add(Intervention Intervention);
        void Update(Intervention Intervention);
        void Delete(int id);


        decimal CalculateInterventionCost(Intervention Intervention, decimal tarifMainOeuvre, decimal tauxTVA);
        bool IsInterventionFree(Reclamation Reclamation);
        IEnumerable<Intervention> GetInterventionsByReclamation(int reclamationId);
    }
}
