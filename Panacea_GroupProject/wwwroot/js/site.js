$(function () {
    function getAuctionIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get('id'); // Assuming the query parameter is named 'id'
    }

    // Extract the auctionId from the URL
    const auctionId = getAuctionIdFromUrl();

    if (auctionId) {
        // Establish the SignalR connection with the auctionId
        var connection = new signalR.HubConnectionBuilder()
            .withUrl(`/auctionHub?auctionId=${auctionId}`)
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connection.start().then(() => {
            console.log("SignalR connected");
        }).catch(err => {
            console.error("SignalR connection error: " + err.toString());
        });

        connection.on("ReceiveMessageChat", function (user, message) {
            console.log("Received message:", user, message); // Log ra console
            const msg = user + ": " + message;
            const li = document.createElement("li");
            li.textContent = msg;
            document.getElementById("messagesList").appendChild(li);
            storeMessageLocally(user, message, auctionId);
        });


        document.getElementById("sendMessageForm").addEventListener("submit", function (event) {
            event.preventDefault();

            const user = document.getElementById("userInput").value;
            const message = document.getElementById("messageInput").value;

            if (message) {
                connection.invoke("SendMessageChat", user, message, auctionId).catch(function (err) {
                    return console.error(err.toString());
                });

                document.getElementById("messageInput").value = "";
            }
        });

        function storeMessageLocally(user, message, auctionId) {
            let allMessages = JSON.parse(sessionStorage.getItem("chatMessages")) || {};
            let auctionMessages = allMessages[auctionId] || [];
            auctionMessages.push({ user: user, message: message });
            allMessages[auctionId] = auctionMessages;
            sessionStorage.setItem("chatMessages", JSON.stringify(allMessages));
        }
        
    } else {
        console.error("Auction ID not found in the URL");
    }
    


    /*connection.start().then(function () {
        console.log("SignalR connected");
    }).catch(function (err) {
        console.error("SignalR connection error: " + err.toString());
    });*/

    connection.on("ReceiveNewBid", function (bid) {
        updateBidTable(bid)
    });

    connection.on("ReceiveAuctionStatusUpdate", function (auctionId, status) {
        refreshAuctionDetails(auctionId, status);
        // Display a popup with the status message
        var message = "";
        if (status === "Pausing") {
            message = "The auction is now pausing."; 
            showAlert(message); 
            showModal(message, false);

        } else if (status === "Processing") {
            message = "The auction is now processing.";  
            showModal(message, true);

        } else if (status === "End") {
            message = "The auction has ended.";
            showAlert(message); // Assuming showAlert function displays an alert
            // Redirect to the Auctions page after showing the message
            window.location.href = '/Template/Auctions';
        }
         
    });

    function refreshAuctionDetails(auctionId, status) {
        refreshDetailSection('#auctionDetail', auctionId, status);
        refreshDetailSection('#placeBidDetail', auctionId, status);
    }

    function refreshDetailSection(sectionSelector, auctionId, status) {
        var section = $(sectionSelector);

        $.ajax({
            url: '/Auctions/BidPrice?id=' + auctionId,
            method: 'GET',
            success: function (result) {
                // Clear existing content
                section.empty();

                // Find and append the updated content
                var detailContent = $(result).find(sectionSelector).html();
                if (detailContent) {
                    section.append(detailContent);
                } else {
                    console.error(`Không tìm thấy phần tử ${sectionSelector} trong dữ liệu trả về từ Razor Page.`);
                }
            },
            error: function (error) {
                console.error('Error fetching section detail:', error);
            }
        });
    }

    function showAlert(message) {
        $('#alertMessage').text(message);
        $('#statusAlert').removeClass('d-none');
    }

    function showModal(message, autoClose) {
        // Set the modal body content and show the modal
        $('#modalBody').text(message);
        $('#statusModal').modal('show');

        if (autoClose) {
            // Auto close the modal after 5 seconds
            setTimeout(function () {
                $('#statusModal').modal('hide');
            }, 5000);
        }
    }
    function updateBidTable(auctionId) {
        var tableBody = $('#bidsTable tbody');

        $.ajax({
            url: '/Auctions/BidPrice?id=' + auctionId,
            method: 'GET',
            success: function (result) {
                // Clear existing table rows
                tableBody.empty();

                var tbodyContent = $(result).find('#bidsTable tbody').html();
                if (tbodyContent) {
                    tableBody.append(tbodyContent);  
                } else {
                    console.error("Không tìm thấy phần tử #tableBody trong dữ liệu trả về từ Razor Page.");
                }
            },
            error: function (error) {
                console.error('Error fetching bids:', error);
            }
        });
    }


});
