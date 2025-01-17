using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(OrderFormViewModel model); // Crée une commande
        Task GetOrderById(int orderId);
        Task<OrderViewModel> GetOrderByIdAsync(int id); // Récupère une commande pour affichage
        Task<IEnumerable<OrderViewModel>> GetOrdersByStatusAsync(bool paid); // Récupère les commandes en fonction du statut de paiement
        Task UpdateOrderPaidStatusAsync(int orderId, bool paid); // Met à jour le statut de paiement

        //admin
        Task<IEnumerable<OrderViewModel>> GetUnpaidOrdersAsync();
        Task SetOrderPaidAsync(int orderId, bool isPaid);

    }
}
