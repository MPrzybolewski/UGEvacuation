using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using UGEvacuationBLL.Services.Interfaces;

namespace UGEvacuationBLL.Services
{
    public class NotificationService : BaseService, INotificationsService
    {
        private readonly FirebaseMessaging _messaging;
        public NotificationService()
        {
            _messaging = FirebaseMessaging.DefaultInstance;
        }
        public async Task SendLocationRequestNotifications(Guid evacuationId, List<string> tokens)
        {
            try
            {
                var messagees = new List<Message>();
                foreach (var token in tokens)
                {
                    messagees.Add(new Message()
                        {
                            Token = token,
                            Notification = new Notification
                            {
                                Title = "Ewakuacja",
                                Body = "Kliknij, aby zobaczyć najlepszą drogę ewakuacji"
                            },
                            Data = new Dictionary<string, string>
                            {
                                ["EvacuationId"] = evacuationId.ToString()
                            }
                        }
                    );
                }
                await _messaging.SendAllAsync(messagees);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Token");
            }
        }

        public async Task SendBestPathNotifications(List<string> tokens, string bestPathString)
        {
            try
            {
                var messagees = new List<Message>();
                foreach (var token in tokens)
                {
                    messagees.Add(new Message()
                        {
                            Token = token,
                            Notification = new Notification
                            {
                                Title = "Ścieżka",
                                Body = "Ścieżka gotowa"
                            },
                            Data = new Dictionary<string, string>
                            {
                                ["Path"] = bestPathString
                            }
                        }
                    );
                }
                await _messaging.SendAllAsync(messagees);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Token");
            }
        }
    }
}