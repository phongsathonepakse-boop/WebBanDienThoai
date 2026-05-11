# Kế hoạch slide báo cáo — WebBanDienThoai (~50 slide)

**Ghi chú:** Bạn có thể sao chép từng slide (tiêu đề + gạch đầu dòng + dòng Hình) sang PowerPoint; nên dùng tỷ lệ **16:9** để chỗ cho ảnh chụp màn hình và sơ đồ rộng rãi. File này chỉ là Markdown, không dùng Word COM.

**Tham chiếu cấu trúc:** Đã căn theo mục lục PDF `Nguyễn_Võ_Trường_Vy_2424802010482.pdf` (Lời mở đầu → Chương 1 tổng quan/khảo sát/bài toán/yêu cầu/công nghệ/use case → Chương 2 phân tích UML/ERD/CSDL → Chương 3 cài đặt & thử nghiệm + giao diện → Chương 4 kết luận), điều chỉnh nội dung cho đề tài **website bán điện thoại** và codebase ASP.NET MVC 5 thực tế.

**Routing dự án:** `RouteConfig` mặc định `{controller}/{action}/{id}`; trang chủ = `PhoneController.Index` → URL thường là `/` hoặc `/Phone` hoặc `/Phone/Index`. Các phân hệ chức năng tương ứng controller: **Phone**, **Cart**, **Checkout**, **Account**, **Admin** (không dùng MVC Areas trong solution hiện tại).

---

## Slide 1 — Trang bìa đồ án
- Tên trường, khoa/viện, môn học/loại đồ án.
- Đề tài: **Xây dựng website bán điện thoại (WebBanDienThoai)**.
- Công nghệ: ASP.NET MVC 5, Entity Framework 6, SQL Server, Bootstrap.
- Họ tên sinh viên, mã SV, lớp, giảng viên hướng dẫn, thành phố — năm 2026.
- Ghi chú: báo cáo kèm mã nguồn solution trong thư mục workspace (đường dẫn có thể in phụ lục).

**Hình:** Logo trường + logo stack công nghệ (tả logo chính thức .NET, SQL Server, Bootstrap) ghép trong PowerPoint hoặc Canva.

## Slide 2 — Mục lục / cấu trúc báo cáo
- Liệt kê Lời mở đầu; Chương 1–4 khớp với chuỗi slide sau (slide 3–5, 6–21, 22–33, 34–48, 49–50).
- Ghi chú số trang/slide ước lượng cho từng chương để ban giám khảo đối chiếu nhanh.
- Có thể thêm mục “Danh mục hình ảnh”, “Danh mục bảng” nếu báo cáo Word kèm theo.
- Đánh số hình theo thứ tự xuất hiện trong slide deck (Hình 1…Hình n).
- Gợi ý: một dòng “Từ khóa: TMĐT, ASP.NET MVC, EF6, Session, mock payment”.

**Hình:** Chụp màn hình chính `/Phone/Index` làm watermark mờ phía sau (tùy chọn), hoặc chỉ typography.

## Slide 3 — Lời mở đầu: Lý do chọn đề tài
- Thương mại điện tử ngành điện thoại/di động phát triển mạnh; nhu cầu kênh bán online.
- Đồ án áp dụng phân tích–thiết kế hướng đối tượng và lập trình web thực tế.
- Phù hợp chương trình: tích hợp CSDL, phân quyền, giỏ hàng, quy trình đặt hàng.
- Chủ đề tập trung một ngành hàng (điện thoại) giúp giới hạn phạm vi nghiệp vụ rõ ràng.
- Có thể mở rộng báo cáo với số liệu thị trường (đề xuất trích dẫn nguồn uy tín).

**Hình:** Biểu đồ cột đơn giản (draw.io): “tỷ lệ người dùng mua điện thoại online” — minh họa, ghi nguồn tham khảo.

## Slide 4 — Lời mở đầu: Mục đích đề tài
- Phân tích và thiết kế hệ thống bán điện thoại trực tuyến cho thương hiệu/shop mẫu **WebBanDienThoai**.
- Cài đặt đầy đủ luồng: duyệt danh mục, chi tiết, đánh giá, giỏ hàng, thanh toán giả lập (Tiền mặt / Chuyển khoản mock).
- Xây dựng phân quyền **User** / **Admin** (đăng nhập Forms Authentication).
- Rèn luyện kỹ năng triển khai CRUD an toàn và xử lý trạng thái giao dịch đơn hàng cơ bản.
- Chuẩn bị nền tảng cho báo cáo chương 2 (UML/ERD) và chương 3 (minh chứng giao diện).

**Hình:** Sơ đồ khối 3 tầng (Presentation – Business – Data) chung cho web MVC, vẽ draw.io.

## Slide 5 — Lời mở đầu: Phạm vi & giới hạn
- **Trong phạm vi:** CRUD sản phẩm (admin), đăng ký/đăng nhập, giỏ session, đơn hàng + chi tiết, đánh giá sản phẩm, thanh toán mock.
- **Giới hạn:** Không cổng thanh toán thật (VNPay/MoMo); không logistics/đổi trả nâng cao; không microservices.
- Công nghệ: một ứng dụng MVC đơn, EF Code First / DB First tùy cấu hình hiện tại.
- Không triển khai mobile native; chỉ web qua trình duyệt (Bootstrap hỗ trợ responsive).
- Phạm vi người dùng: tiếng Việt/Anh trên giao diện tùy nhóm đã chỉnh trong View (ghi đúng thực tế).

**Hình:** Bảng 2 cột “Có / Không” trong PowerPoint hoặc draw.io.

---

## Slide 6 — Chương 1: Tổng quan đề tài (mở đầu chương)
- Mục tiêu chương: nêu bối cảnh, bài toán, yêu cầu, công nghệ, mô hình hóa use case.
- Liên hệ trực tiếp với repository **WebBanDienThoai** (Visual Studio solution).
- Cấu trúc chương 1 song song mục lục PDF tham chiếu (1.1–1.5).
- Nêu rõ đối tượng sử dụng: sinh viên báo cáo + giảng viên đánh giá + (tuỳ chọn) doanh nghiệp demo.

**Hình:** Icon chương hoặc ảnh minh họa cửa hàng điện thoại (stock ảnh có license).

## Slide 7 — Khảo sát hiện trạng thị trường / đối thủ
- Các sàn/website bán điện thoại phổ biến; điểm mạnh: lọc, so sánh, thanh toán đa dạng.
- Bài học cho đồ án: cần danh sách rõ, chi tiết, giỏ hàng ổn định, admin quản lý nhanh.
- Khảo sát giao diện: menu, thẻ sản phẩm, trang chi tiết, luồng thanh toán (chỉ quan sát, không sao chép giao diện).
- Rút ra 3–5 tính năng “chuẩn thị trường” mà đồ án **WebBanDienThoai** đang đáp ứng một phần.
- Ghi nhận khoảng trống: thanh toán thật, quản kho, vận chuyển — dùng cho mục hạn chế chương 4.

**Hình:** Collage logo 2–3 website tham chiếu (tuân thủ bản quyền logo) hoặc bảng so sánh tính năng.

## Slide 8 — Mô tả bài toán WebBanDienThoai
- **Tác nhân chính:** Khách (vãng lai / đã đăng ký), Quản trị viên.
- **Dữ liệu cốt lõi:** Điện thoại (`Phone`), đơn hàng (`Order`, `OrderDetail`), người dùng (`UserAccount`), đánh giá (`Review`).
- **Luồng nghiệp vụ:** Xem → Thêm giỏ → Checkout → Lưu đơn → Xác nhận mock thanh toán.
- **Ràng buộc:** Đặt hàng yêu cầu đủ họ tên, SĐT, địa chỉ; `paymentMethod` chỉ nhận `Cash` hoặc `Bank` (theo `CheckoutController`).
- **Đầu ra:** Trang thành công hiển thị thông tin đơn và phương thức thanh toán đã chọn (mock).

**Hình:** Sơ đồ use case khối đơn giản (chưa chi tiết) — draw.io, chữ tiếng Việt.

## Slide 9 — Yêu cầu chức năng — tổng quan
- Quản lý sản phẩm điện thoại (hiển thị, lọc theo `category`, tìm theo tên).
- Giỏ hàng lưu **Session**, cập nhật số lượng, xóa dòng hàng.
- Đặt hàng với thông tin giao hàng; phương thức **Cash** hoặc **Bank** (mô phỏng).
- Đánh giá sản phẩm sau khi xem chi tiết (POST `AddReview`).
- Phân quyền: khu vực `/Admin/*` chỉ role Admin.

**Hình:** Danh sách bullet dạng icon — có thể thiết kế trong PowerPoint SmartArt.

## Slide 10 — Yêu cầu chức năng — chi tiết theo controller
- **Phone:** `Index` (trang chủ + lọc `?category=`), `Search?query=`, `Details/{id}`, form đánh giá (action `AddReview`).
- **Cart:** `Add/{id}`, `Index`, `Remove/{id}`, `Decrease`, `SetQuantity` (POST), `UpdateQuantity` (JSON), `Clear`, `Checkout` (view trong `CartController` nếu dùng).
- **Checkout:** `Index` GET/POST, `Success` sau khi đặt hàng.
- **Account:** `Register`, `Login`, **`/Account/Logout`** (đăng xuất trong `AccountController`).
- **Admin:** `Index`, `ManageProducts`, `Create`, `Edit`, `Delete` / `DeleteConfirmed`.

**Hình:** Bảng “Chức năng – URL – Controller.Action” chụp từ tài liệu này hoặc vẽ lại trong Word/PPT.

## Slide 11 — Yêu cầu phi chức năng
- Giao diện responsive với **Bootstrap**; thời gian phản hồi chấp nhận được trên IIS Express.
- Bảo mật cơ bản: `[Authorize(Roles = "Admin")]`, `[ValidateAntiForgeryToken]` trên POST quan trọng, mật khẩu băm + salt (`UserAccount`).
- Khả năng mở rộng: thêm cổng thanh toán, tồn kho thực.
- Khả năng bảo trì: mã theo chuẩn MVC (Controller/View/Model tách file).
- Tính sử dụng: thông báo `TempData` cho lỗi/thành công sau redirect.

**Hình:** Checklist NFR — PowerPoint.

## Slide 12 — Công nghệ: ASP.NET MVC 5 & C#
- Mô hình **MVC**: View Razor, Controller xử lý HTTP, Model binding.
- Routing mặc định trong `App_Start/RouteConfig.cs`: default **Phone/Index**.
- Tách bạch logic đọc/ghi qua `AppDbContext` (EF6).
- Ngôn ngữ **C#** trên .NET Framework (ghi đúng phiên bản trong file `.csproj` khi báo cáo).
- Gợi ý slide phụ: so sánh Web Forms vs MVC (1 đoạn ngắn).

**Hình:** Logo ASP.NET MVC + sơ đồ request pipeline (MS Docs — trích và ghi nguồn).

## Slide 13 — Công nghệ: Entity Framework 6 & SQL Server
- `DbSet`: `Phones`, `Orders`, `OrderDetails`, `Reviews`, `Users`.
- Chuỗi kết nối `name=AppDbContext` trong `Web.config`.
- Migration / tạo bảng theo quy trình môn học (ghi rõ nhóm đã làm bước nào).
- LINQ to Entities trong controller (ví dụ lọc `Where`, `Sum` giỏ hàng).
- Sao lưu / script `.bak` hoặc script seed (nếu nhóm có) — ghi trong phụ lục.

**Hình:** Screenshot **SQL Server Object Explorer** hoặc SSMS: danh sách bảng DB thực tế của project.

## Slide 14 — Công nghệ: Bootstrap & Session giỏ hàng
- Bootstrap cho layout, form, navbar.
- Giỏ hàng: `Session["Cart"]` kiểu `List<CartItem>` trong `CartController`.
- TempData cho thông báo sau redirect (`TempData["Success"]`, `TempData["Error"]`, …).
- Giới hạn số lượng tối đa mỗi dòng (ví dụ `Math.Min(qty, 50)` trong `SetQuantity`) — trích đúng code khi trình bày.
- Cookie xác thực (Forms) cho phiên đăng nhập — bổ sung nếu báo cáo có mục bảo mật.

**Hình:** Screenshot DevTools → Application → Cookies/Session (nếu có) hoặc slide giải thích luồng session — draw.io.

## Slide 15 — Mô hình hóa: Xác định Actor
- **Khách:** xem sản phẩm, đăng ký, mua hàng, đánh giá.
- **Admin:** đăng nhập quyền Admin, CRUD phone, xem dashboard thống kê đơn giản trên `Admin/Index`.
- **Hệ thống thanh toán (mock):** chỉ ghi nhận lựa chọn Cash/Bank, không gọi API ngân hàng.
- **Actor phụ (tuỳ chọn):** “Hệ thống email” nếu mở rộng báo cáo — hiện tại chưa triển khai.
- Ranh giới hệ thống: biên là trình duyệt + IIS + SQL Server.

**Hình:** Stick figure diagram — draw.io, 3 actor.

## Slide 16 — User stories (ví dụ)
- “Là khách, tôi muốn lọc điện thoại theo danh mục để tìm nhanh.” (`/Phone/Index?category=...`)
- “Là khách, tôi muốn thêm vào giỏ và chỉnh số lượng trước khi thanh toán.” (`/Cart/...`)
- “Là admin, tôi muốn thêm/sửa/xóa sản phẩm.” (`/Admin/ManageProducts`, `Create`, `Edit`, `Delete`)
- “Là khách, tôi muốn xem đánh giá và gửi đánh giá sau khi xem chi tiết.” (`/Phone/Details/{id}`)
- Ưu tiên (MoSCoW): Must / Should / Could — điền theo nhóm.

**Hình:** Thẻ user story (template màu) — PowerPoint.

## Slide 17 — Danh sách Use Case chính
- UC Đăng ký, Đăng nhập, Đăng xuất.
- UC Tìm kiếm/Lọc sản phẩm, Xem chi tiết, Gửi đánh giá.
- UC Quản lý giỏ hàng, Đặt hàng, Xác nhận đơn (Success).
- UC Admin: quản lý sản phẩm, phân quyền truy cập.
- Gom nhóm UC theo package: “Khách hàng”, “Quản trị”, “Thanh toán mock”.

**Hình:** Bảng UC | Mô tả ngắn | Actor — Word/PPT.

## Slide 18 — Sơ đồ Use Case tổng quan
- Hệ thống **WebBanDienThoai** ở giữa; Khách và Admin kết nối các cụm UC.
- Nhóm include/extend nếu cần (ví dụ “Đăng nhập” include cho “Quản trị” — bắt buộc Admin).
- Ghi chú: khách vãng lai vẫn xem và mua được tới mức nào (theo logic thực tế đã cài — trình bày trung thực).
- Ranh giới hệ thống (system boundary) hình chữ nhật bao quanh các UC.
- Phiên bản sơ đồ + ngày cập nhật (footer slide).

**Hình:** **draw.io** — xuất PNG/SVG; đặt tiêu đề “Hình X — Sơ đồ Use Case tổng quan”.

## Slide 19 — Đặc tả Use Case: Đăng nhập / Đăng ký
- Tiền điều kiện, hậu điều kiện, luồng chính, luồng phụ (sai mật khẩu, trùng username).
- Liên kết code: `AccountController.Login`, `Register`; role redirect Admin → `Admin/Index`, User → `Phone/Index`.
- Đăng ký: tạo `UserAccount` với `Role = "User"`, lưu salt + hash mật khẩu.
- Đăng nhập thất bại: `ModelState.AddModelError` hiển thị trên view.
- Đăng xuất: `Logout` xóa cookie xác thực (mô tả theo code thực tế).

**Hình:** Screenshot **`/Account/Login`** và **`/Account/Register`** (Chrome full width).

## Slide 20 — Đặc tả Use Case: Đặt hàng & thanh toán mock
- Điều kiện giỏ không rỗng; POST `Checkout/Index` với `paymentMethod` = `Cash` | `Bank`.
- Tạo `Order` + các `OrderDetail`, clear session giỏ, redirect `Checkout/Success`.
- Nếu thiếu tên/SĐT/địa chỉ: `TempData["Error"]` và ở lại form (theo controller).
- Lưu snapshot giá vào `OrderDetail.Price` để không bị ảnh hưởng khi admin đổi giá sau này.
- `TempData` chuyển `OrderId`, tổng tiền, nhãn phương thức thanh toán sang trang Success.

**Hình:** Screenshot form **`/Checkout/Index`** (chọn Cash/Bank) + trang **`/Checkout/Success`** ghép 2 ô.

## Slide 21 — Đặc tả Use Case: Quản trị sản phẩm
- `[Authorize(Roles = "Admin")]` trên `AdminController`.
- Luồng: `ManageProducts` → `Create` / `Edit` / `Delete` + `DeleteConfirmed` POST.
- Tìm kiếm/lọc: tham số `search`, `category` trên `ManageProducts`.
- Thông báo: `TempData["StatusMessage"]` sau tạo/sửa/xóa thành công.
- Rủi ro: xóa `Phone` có thể ảnh hưởng `Review`/đơn hàng — ghi nhận nếu chưa có FK cascade trong DB.

**Hình:** Screenshot **`/Admin/ManageProducts`** (có thanh tìm kiếm/lọc nếu hiển thị).

---

## Slide 22 — Chương 2: Phân tích hệ thống (mở đầu chương)
- Chuyển từ yêu cầu sang thiết kế: UML (gói nhìn sinh viên), CSDL.
- Công cụ đề xuất: Visual Studio, SSMS, draw.io, PlantUML (tùy nhóm).
- Cấu trúc chương 2 bám PDF tham chiếu: class, sequence, activity, ERD, thiết kế bảng.
- Liệt kê rủi ro thiết kế: trùng lặp logic giỏ hàng giữa `Cart` và `Checkout` — ghi nhận nếu có.

**Hình:** Sơ đồ lớp khung chương — placeholder draw.io.

## Slide 23 — Kiến trúc tổng thể MVC & luồng HTTP
- Browser → IIS/IIS Express → Routing → Controller → View/JSON.
- `AppDbContext` gói truy cập EF đến SQL Server.
- View Razor render HTML; static file `Content`/`Scripts` qua bundling hoặc trực tiếp (theo project).
- Gói nhìn triển khai: một assembly web, không tách API riêng.
- Ghi chú cổng: URL `{controller}/{action}/{id}` mặc định MVC.

**Hình:** Sơ đồ deployment đơn giản (1 server, 1 DB) — draw.io.

## Slide 24 — Class Diagram (thiết kế / ánh xạ entity)
- Lớp thực thể: `Phone`, `Order`, `OrderDetail`, `Review`, `UserAccount`, `CartItem` (session model).
- Quan hệ: Order 1–n OrderDetail; Phone 1–n Review; OrderDetail n–1 Phone.
- ViewModel: `LoginViewModel`, `RegisterViewModel` (tách khỏi entity persistence).
- Ghi chú: `CartItem` không map bảng — chỉ tồn tại trong session.
- Quy ước đặt tên navigation property (nếu có trong model — kiểm tra file).

**Hình:** **draw.io** Class Diagram — xuất PNG; căn chỉnh multiplicity.

## Slide 25 — Sequence Diagram: Đăng nhập
- Người dùng gửi POST `/Account/Login` kèm `ValidateAntiForgeryToken`.
- `AccountController` tìm user trong `Users`, gọi `VerifyPassword` (hash + salt).
- Thành công: thiết lập cookie xác thực và redirect theo role (Admin vs User).
- Thất bại: trả lại view kèm lỗi validation.
- Tùy chọn: lifeline “Membership” nếu tách lớp service (hiện tại có thể gộp trong controller).

**Hình:** draw.io sequence; 4–5 lifeline: User, Browser, Controller, DbContext, DB.

## Slide 26 — Sequence Diagram: Thêm giỏ & Checkout
- GET `/Cart/Add/{id}` → đọc `Session["Cart"]` → thêm/cập nhật `CartItem` → redirect `Cart/Index`.
- POST `/Checkout/Index` → validate → `Orders.Add` → `OrderDetails` → `SaveChanges` → clear cart.
- Nếu giỏ rỗng: redirect về `Cart/Index` hoặc thông báo (theo đúng nhánh code).
- `SaveChanges` có thể gọi hai lần (tạo Order rồi thêm chi tiết) — phản ánh đúng `CheckoutController` nếu vậy.
- Kết thúc: HTTP 302 tới `/Checkout/Success`.

**Hình:** draw.io — một diagram hoặc tách 2 swimlane.

## Slide 27 — Sequence Diagram: Admin tạo/sửa sản phẩm
- Admin → `Admin/Create` POST → validate `ModelState` → `db.Phones.Add` / `Entry.State = Modified` → `SaveChanges` → redirect `ManageProducts`.
- GET `Create`/`Edit` trả form rỗng / điền sẵn entity.
- Xóa: GET `Delete` hiển thị xác nhận, POST `DeleteConfirmed` xóa khỏi `db.Phones`.
- Mọi thao tác ghi đều qua EF `DbContext` trong cùng controller.
- Bắt buộc role Admin — hiển thị 401/redirect login nếu chưa xác thực.

**Hình:** draw.io sequence với alt fragment (Create vs Edit).

## Slide 28 — Activity Diagram: Quy trình mua hàng end-to-end
- Quyết định: giỏ rỗng? đã điền đủ thông tin? phương thức thanh toán hợp lệ?
- Kết thúc: trang thành công hoặc quay lại kèm `TempData`.
- Nhánh song song: chọn Cash vs Bank (cùng kết quả lưu đơn, khác nhãn hiển thị mock).
- Hoạt động phụ: thêm đánh giá sau khi xem chi tiết (tách swimlane “Tùy chọn”).
- Swimlane Khách / Hệ thống / Admin (nếu minh họa đủ rộng).

**Hình:** draw.io activity — nhánh Cash/Bank song song rồi hội tụ.

## Slide 29 — ERD (mức khái niệm)
- Thực thể và khóa: `Phone(Id,...)`, `Order(Id,...)`, `OrderDetail(OrderId, PhoneId,...)`, `Review`, `UserAccount`.
- Ghi chú FK: `OrderDetail.OrderId` → `Order`; `OrderDetail.PhoneId` → `Phone`; `Review.PhoneId` → `Phone`.
- Cardinality: mỗi đơn có nhiều dòng chi tiết; mỗi máy có nhiều review.
- Ghi chú: `UserAccount` độc lập với `Order` trong model hiện tại (đơn lưu tên/SĐT dạng text — trung thực với code).
- Khóa chính/phụ: in đậm PK, gạch chân FK.

**Hình:** draw.io ERD notation (Crow’s foot) — xuất khổ ngang cho slide 16:9.

## Slide 30 — Thiết kế CSDL: từ logic đến vật lý
- Tên bảng theo convention EF (có thể pluralize `Phones`, `Orders`, … — kiểm tra SSMS thực tế).
- Index gợi ý: tìm theo `Name`, `Category` (đề xuất tối ưu, không bắt buộc đã có trong đồ án).
- Chuẩn hóa: OrderDetail tách dòng hàng khỏi Order header.
- Kiểu dữ liệu: `decimal` giá, `int` số lượng, `datetime` ngày đặt.
- Sao lưu / restore DB cho demo báo cáo (ghi quy trình ngắn).

**Hình:** Screenshot schema diagram trong SSMS (Database Diagrams) nếu tạo được; không thì dùng lại Hình ERD slide 29.

## Slide 31 — Cấu trúc bảng: Phone & Review
- Các cột chính của `Phone` (tên, giá, danh mục, mô tả, URL ảnh, … — mở file `Models/Phone.cs` để liệt kê chính xác khi báo cáo).
- `Review`: liên kết `PhoneId`, nội dung, điểm `rating`, thời gian (nếu có).
- Ràng buộc toàn vẹn: review không tồn tại nếu xóa phone (tuỳ cấu hình FK — kiểm tra migration).
- Dữ liệu mẫu: ảnh URL ngoài hoặc local — ghi rõ nguồn.
- Truy vấn mẫu: `SELECT` kèm `JOIN` Phone–Review.

**Hình:** Screenshot bảng trong SSMS (Design hoặc kết quả `SELECT TOP`).

## Slide 32 — Cấu trúc bảng: Order, OrderDetail, UserAccount
- `Order`: thông tin khách, ngày đặt, địa chỉ/ghi chú (theo code `CheckoutController`: gộp phone vào chuỗi địa chỉ — ghi nhận thiết kế).
- `OrderDetail`: số lượng, đơn giá snapshot.
- `UserAccount`: username, role, salt/hash password.
- Phân tích độ chuẩn hóa: Order lưu chuỗi địa chỉ ghép — ưu/nhược điểm khi báo cáo.
- Ví dụ 1 đơn có 2 dòng `OrderDetail` khác `PhoneId`.

**Hình:** SSMS — 3 bảng hoặc script `CREATE TABLE` trong slide phụ lục.

## Slide 33 — Ma trận chức năng – dữ liệu (CRUD)
- Hàng: chức năng chính; cột: Phone, Order, OrderDetail, Review, User.
- Đánh dấu C/R/U/D và “session-only” cho giỏ hàng.
- Thêm cột “Ghi chú bảo mật” (ai được R/W).
- Ma trận giúp giảng viên đối chiếu nhanh phạm vi đồ án.
- Cập nhật ma trận khi bổ sung tính năng (phiên bản v1.0).

**Hình:** Bảng PowerPoint; có thể tô màu.

---

## Slide 34 — Chương 3: Cài đặt và thử nghiệm (mở đầu chương)
- Môi trường: Windows, Visual Studio, .NET Framework (phiên bản theo project), SQL Server LocalDB/Express.
- Cấu trúc solution: thư mục `Controllers`, `Models`, `Views`, `App_Start`, `Content`, `Scripts`.
- Bước clone/build: Restore NuGet, build solution, chạy migration/seed nếu có.
- Ghi phiên bản Git commit (hash ngắn) nếu báo cáo có phụ lục mã nguồn.
- Chuẩn bị tài khoản demo: 1 user thường + 1 admin (theo seed `EnsureDefaultAdmin` trong code).

**Hình:** Screenshot **Solution Explorer** trong Visual Studio (mở rộng cây thư mục chính).

## Slide 35 — Cấu hình chạy ứng dụng & connection string
- File `Web.config`: `connectionStrings` → `AppDbContext`.
- Khởi chạy F5; URL base (ví dụ `https://localhost:xxxxx/`).
- Kiểm tra port SSL trong project properties nếu team dùng HTTPS nội bộ.
- Ghi chú `compilation debug=true/false` khi demo vs bàn giao.
- Sao lưu `Web.config` trước khi đổi chuỗi kết nối máy khác.

**Hình:** Screenshot **Web.config** (che giấu mật khẩu server nếu deploy thật).

## Slide 36 — Thử nghiệm chức năng (kế hoạch test)
- Bảng test case: ID, bước, dữ liệu vào, kết quả mong đợi, pass/fail.
- Ví dụ: thêm giỏ → checkout Cash → có `OrderId` trên `Success`.
- Thử nghiệm âm: checkout với giỏ rỗng, `paymentMethod` sai, bỏ trống địa chỉ.
- Thử nghiệm admin: CRUD sản phẩm và kiểm tra hiển thị ở `/Phone/Index`.
- Thử nghiệm phân quyền: truy cập `/Admin/Index` khi chưa login.

**Hình:** Bảng Excel chụp màn hình hoặc nhập trực tiếp PowerPoint Table.

## Slide 37 — Giao diện: Trang chủ / danh sách điện thoại
- Slider/banner khi `ViewBag.IsHomePage == true` (logic trong `Phone/Index`).
- Lưới sản phẩm Bootstrap.
- Ghi chú route mặc định: vào `/` cũng ra `Phone/Index`.
- Có thể chụp thêm mobile width (F12 responsive) để minh họa Bootstrap.
- Đánh dấu vùng navbar / footer nếu layout có.

**Hình:** Screenshot **`/`** hoặc **`/Phone/Index`** (trình duyệt Chrome, thanh địa chỉ hiển thị URL).

## Slide 38 — Giao diện: Lọc theo danh mục
- URL mẫu: **`/Phone/Index?category=<TênDanhMục>`** (giá trị đúng theo dữ liệu seed trong DB).
- So sánh trước/sau lọc (2 ảnh nhỏ).
- Giải thích tham số `category` được `Trim()` trong controller trước khi so sánh.
- Ghi tên danh mục thật từ DB (ví dụ “iPhone”, “Android” — tùy seed).
- Kiểm tra trường hợp `category` rỗng / sai.

**Hình:** 2 screenshot cạnh nhau hoặc một ảnh có chú thích URL.

## Slide 39 — Giao diện: Tìm kiếm sản phẩm
- **`/Phone/Search?query=iphone`** (hoặc từ khóa có trong DB).
- Hiển thị `ViewBag.SearchQuery` và danh sách kết quả trên view `Index`.
- So sánh với tìm theo form GET/POST trên layout (chụp đúng control người dùng dùng).
- Trường hợp không có kết quả: chụp màn hình trống + thông báo (nếu view có).
- Ghi chú tìm theo `Name.Contains` trong code.

**Hình:** Screenshot thanh tìm kiếm + kết quả (URL bar hiển thị query).

## Slide 40 — Giao diện: Chi tiết điện thoại & đánh giá
- **`/Phone/Details/{id}`** với `id` hợp lệ từ danh sách.
- Form đánh giá gọi POST **`AddReview`** (xem `PhoneController` và view `Details` để chụp đúng vị trí form).
- Hiển thị danh sách review hiện có dưới chi tiết (nếu view render).
- Thử gửi đánh giá 1–5 sao và reload trang xác nhận lưu DB.
- URL sau khi gửi: thường redirect về Details (kiểm tra action thực tế).

**Hình:** Screenshot cả trang Details + phần sao đánh giá/bình luận.

## Slide 41 — Giao diện: Giỏ hàng
- **`/Cart/Index`** sau khi đã `/Cart/Add/{id}`.
- Hiển thị subtotal, nút tăng/giảm, xóa, (nếu có) link sang checkout.
- Thử `SetQuantity` hoặc nút `Decrease` để minh họa cập nhật session.
- Thử `Clear` hoặc xóa từng dòng (`Remove/{id}`) và chụp thông báo `TempData`.
- Hiển thị `ViewBag.TotalQuantity` nếu có trên view.

**Hình:** Screenshot giỏ có ít nhất 2 dòng sản phẩm (URL `/Cart/Index`).

## Slide 42 — Giao diện: Checkout — nhập thông tin & chọn thanh toán
- **`/Checkout/Index`** GET: form tên, SĐT, địa chỉ, `paymentMethod` Cash/Bank, ghi chú.
- Giải thích đây là **mock** (không trừ tiền thật).
- Liên hệ code: validate `paymentMethod` chỉ `Cash` hoặc `Bank`.
- Ghi chú trường `note` được nối vào chuỗi địa chỉ khi lưu (theo `CheckoutController`).
- So sánh với trang `/Cart/Checkout` nếu layout dẫn người dùng qua bước trung gian.

**Hình:** Screenshot full form trước khi bấm Đặt hàng (`/Checkout/Index`).

## Slide 43 — Giao diện: Hoàn tất đơn hàng
- **`/Checkout/Success`** sau POST thành công; hiển thị `TempData` về mã đơn, tổng tiền, phương thức (theo view thực tế).
- Đối chiếu bản ghi mới trong bảng `Orders` / `OrderDetails` trên SSMS (slide phụ hoặc chú thích).
- Kiểm tra session giỏ đã `null` sau đặt hàng.
- Thử lặp lại POST (F5) để bàn luận idempotency — nếu cần slide phụ kiến trúc.
- Che PII (họ tên/SĐT thật) khi chụp demo.

**Hình:** Screenshot trang Success (`/Checkout/Success`).

## Slide 44 — Giao diện: Đăng ký tài khoản
- **`/Account/Register`** — các field theo `RegisterViewModel`.
- Hiển thị validation: email/độ dài mật khẩu — theo attribute trong ViewModel.
- Sau đăng ký thành công: luồng redirect/login (mô tả đúng code).
- Thử đăng ký trùng username để chụp lỗi “already registered”.
- So sánh với tài khoản seed admin (không hardcode mật khẩu trên slide).

**Hình:** Screenshot form đăng ký + thông báo validation (submit trống hoặc sai định dạng).

## Slide 45 — Giao diện: Đăng nhập & phân quyền
- **`/Account/Login`**; redirect admin tới **`/Admin/Index`** sau khi đăng nhập thành công.
- User thường vào **`/Phone/Index`** sau login (theo `AccountController`).
- Thử login sai để hiện lỗi “Invalid username or password”.
- Thử truy cập `/Admin/ManageProducts` bằng user thường — kỳ vọng bị chặn.
- **`/Account/Logout`**: chụp trạng thái sau đăng xuất (mất menu admin).

**Hình:** Screenshot login + ảnh nhỏ `Admin/Index` sau đăng nhập admin (2 ô).

## Slide 46 — Giao diện: Admin — Dashboard & quản lý sản phẩm
- **`/Admin/Index`**: thống kê nhanh `ViewBag` (số SP, tổng giá trị, số danh mục).
- **`/Admin/ManageProducts`**: danh sách, ô tìm kiếm, dropdown category nếu có.
- Thử filter `?search=` và `?category=` đồng thời để minh họa.
- Ghi chú quyền `[Authorize(Roles = "Admin")]` trên toàn controller.
- Liên kết nhanh tới `Create` từ giao diện quản lý (nếu có nút).

**Hình:** 2 ảnh: `/Admin/Index` + `/Admin/ManageProducts` (URL rõ ràng).

## Slide 47 — Giao diện: Admin — Thêm / Sửa / Xóa sản phẩm
- **`/Admin/Create`**, **`/Admin/Edit/{id}`**, **`/Admin/Delete/{id}`** (xác nhận xóa).
- Hiển thị anti-forgery token trên form (view source hoặc devtools — tùy chọn).
- Sau `Create`: sản phẩm xuất hiện trên `/Phone/Details/{id}` mới.
- Sau `Edit`: giá/mô tả đổi trên trang khách.
- `DeleteConfirmed`: redirect kèm `TempData["StatusMessage"]`.

**Hình:** 3 screenshot hoặc collage (`Create`, `Edit`, `Delete`); ghi URL từng ảnh.

## Slide 48 — Giao diện bổ sung (luồng giỏ & trang tĩnh)
- **`/Home/About`**, **`/Home/Contact`** nếu layout điều hướng còn dùng.
- **`/Cart/Checkout`** (GET trong `CartController`): chụp khi giỏ không rỗng; so sánh với `/Checkout/Index`.
- Giải thích vì sao có hai màn “checkout” (nếu cả hai đều tồn tại): mục đích UX hoặc lịch sử refactor.
- **`/Cart/UpdateQuantity`**: có thể chụp Network tab JSON (tùy chọn nâng cao).
- Điều hướng navbar: liệt kê các link chính đã bật trong `_Layout`.

**Hình:** Screenshot từng route; ghi URL đầy đủ dưới ảnh (localhost + path).

---

## Slide 49 — Chương 4: Kết luận — Kết quả & hạn chế
- **Kết quả:** Hoàn thành website MVC bán điện thoại với các luồng đã liệt kê; minh chứng bằng slide chương 3.
- **Hạn chế:** Thanh toán mock; chưa tích hợp kho thực; đánh giá chưa kiểm duyệt; session mất khi hết session server.
- **Thuận lợi:** Stack phổ biến, tài liệu Microsoft đầy đủ, phù hợp demo giảng viên.
- **Khó khăn:** Đồng bộ session giỏ, thiết kế CSDL đơn giản hóa thông tin đơn hàng, kiểm thử edge case.
- Đánh giá mức độ đạt yêu cầu môn học (theo rubric nếu có).

**Hình:** Biểu đồ “% hoàn thành yêu cầu” (ước lượng) — PowerPoint.

## Slide 50 — Hướng phát triển, tài liệu tham khảo & lời cảm ơn
- Hướng phát triển: tích hợp VNPay/MoMo, quản lý tồn kho, email xác nhận đơn, API REST, Docker.
- Tài liệu: Microsoft Docs MVC5, EF6, Bootstrap; giáo trình môn học.
- Lời cảm ơn giảng viên và nhóm (nếu có).
- Tuân thủ bản quyền hình ảnh sản phẩm (URL ngoài vs ảnh nội bộ).
- Khuyến nghị triển khai IIS/ Azure SQL cho bản production.

**Hình:** QR mã nguồn GitHub (nếu public) hoặc ảnh nhóm — tùy chọn.

---

**Tổng số slide nội dung:** **50** (đã đánh số từ 1 đến 50).
