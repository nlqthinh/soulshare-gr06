﻿using Microsoft.AspNet.SignalR;
using SoulShare_Group06.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoulShare_Group06.Controllers
{
    public class RoomController : Controller
    {
        private readonly DBContextSoulShare db = new DBContextSoulShare();

        public ActionResult Index()
        {
            try
            {
                // Retrieve user and room information from the session or database
                var user = (SoulShare_Group06.Models.Customer)System.Web.HttpContext.Current.Session["user"];
                var room = (SoulShare_Group06.Models.Room)System.Web.HttpContext.Current.Session["room"];

                // Check if user and room are not null before assigning to ViewBag
                if (user != null && room != null)
                {
                    ViewBag.User = user;
                    ViewBag.Room = room;

                    // Your existing code here
                    return View();
                }
                else
                {
                    // Handle the case when user or room is null, for example, redirect to another page
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                // For simplicity, I'm setting an error message in TempData
                TempData["Error"] = "An error occurred: " + ex.Message;

                // Redirect to HomeController/Index
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult StartChat()
        {
            Room existingRoom = db.Rooms.FirstOrDefault(r => r.room_status == 0);

            if (existingRoom == null)
            {
                Room newRoom = new Room { room_status = 0 };
                db.Rooms.Add(newRoom);
                db.SaveChanges();

                int newRoomId = newRoom.room_id;
                AssignUserToRoom(newRoomId);

                return RedirectToAction("Index");
            }
            else
            {
                int existingRoomId = existingRoom.room_id;
                AssignUserToRoom(existingRoomId);
                return RedirectToAction("Index");            }
        }

        private ActionResult AssignUserToRoom(int roomId)
        {
            int currentUserId = GetCurrentUserId();
            if (currentUserId == -1)
            {
                return RedirectToAction("Index", "Home");

            }
            Room room = db.Rooms.Find(roomId);

            if (room != null)
            {
                if (room.user1 == null)
                {
                    room.user1 = currentUserId;
                }
                else if (room.user2 == null)
                {
                    room.user2 = currentUserId;
                }
                if (room.user1 != null && room.user2 != null)
                {
                    room.room_status = 1;
                }
                try
                {
                    Session["room"] = room;
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        private int GetCurrentUserId()
        {
            SoulShare_Group06.Models.Customer userObject = (SoulShare_Group06.Models.Customer)System.Web.HttpContext.Current.Session["user"];
            string userEmail = userObject.customer_email;
            int userId = GetUserIdByEmail(userEmail);
            return userId;
        }

        private int GetUserIdByEmail(string userEmail)
        {
            var user = (from u in db.Customers
                        where u.customer_email == userEmail
                        select u).FirstOrDefault();

            if (user != null)
            {
                return user.customer_id;
            }
            else
            {
                // Redirect to Index action in HomeController
                return -1; // You can choose any unique identifier or flag to indicate not found
            }
        }

        public ActionResult QuitRoom()
        {
            int currentUserId = GetCurrentUserId();
            if (currentUserId == -1)
            {
                return RedirectToAction("Index", "Home");
            }

            // Find the room where the user is located
            Room userRoom = db.Rooms.FirstOrDefault(r => r.user1 == currentUserId || r.user2 == currentUserId);

            if (userRoom != null)
            {
                // Check if the user is in user1 or user2 and set the respective field to NULL
                if (userRoom.user1 == currentUserId)
                {
                    userRoom.user1 = null;
                }
                else if (userRoom.user2 == currentUserId)
                {
                    userRoom.user2 = null;
                }

                if (userRoom.user1 == null || userRoom.user2 == null)
                {
                    userRoom.room_status = 0; 
                }

                try
                {
                    db.SaveChanges();
                    // Clear the room session variable
                    System.Web.HttpContext.Current.Session["room"] = null;
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return RedirectToAction("Index", "Home"); // Redirect to home as a fallback
                    throw;
                }
            }

            return RedirectToAction("Index", "Home");
        }

    }
}