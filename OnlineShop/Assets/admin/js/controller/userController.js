var user = {
	// phương thức init của user
	init: function () {
		user.registerEvents();
	},
	// phương thức gọi sự kiện, toàn bộ sự kiện viết trong phương thức này
	registerEvents: function () {
		$('.btn-active').off('click').on('click', function (e) {
			e.preventDefault();
			var btn = $(this);
			var id = btn.data('id');
			$.ajax({
				url: "/User/ChangeStatus",
				data: { id: id },
				dataType: "json",
				type: "POST",
				success: function (response) {
					console.log(response);
					if (response == true) {
						btn.text('Kích hoạt');
					} else {
						btn.text('Khóa');
					}
				}
			});
		});
	}
}
user.init();