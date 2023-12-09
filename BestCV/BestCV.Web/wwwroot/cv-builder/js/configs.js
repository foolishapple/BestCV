templates = {
    'thanh-dat': `
        <div class="cv-row cv-row-empty">
            <div class="cv-col cv-col-4" onmouseenter="onMouseEnterColumn(this)"
                onmouseleave="onMouseLeaveColumn(this)">

                <div class="cv-item cv-item-avatar movable padding">
                    <div class="cv-item-body" style="border: 2px solid #FFB156;">
                        <img src="/cv-builder/images/girl1.jpeg" alt="">
                    </div>
                </div>

                <div class="cv-item cv-item-contact-info movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="color: white">
                            <b>THÔNG TIN CÁ NHÂN</b>
                        </div>
                    </div>
                    <div class="cv-item-body padding">
                        <div class="cv-item-email">
                            <i class="fa fa-envelope"></i>
                            <span contenteditable="true" onclick="onClickElement(this)">thao.dinh@dion.vn</span>
                        </div>

                        <div class="cv-item-phone">
                            <i class="fa fa-circle-phone"></i>
                            <span contenteditable="true" onclick="onClickElement(this)">0906 799 9xx</span>
                        </div>

                        <div class="cv-item-website">
                            <i class="fa fa-globe"></i>
                            <span contenteditable="true"
                                onclick="onClickElement(this)">facebook.com/thao.dinh</span>
                        </div>

                        <div class="cv-item-address">
                            <i class="fa fa-location-dot"></i>
                            <span contenteditable="true" onclick="onClickElement(this)">Hoàng Mai - Hà
                                Nội</span>
                        </div>
                    </div>
                </div>

                <div id="cv-item-skill" class="cv-item movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="color: white">
                            <b>KỸ NĂNG</b>
                        </div>
                    </div>
                    <div class="cv-item-body padding">
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)">
                            Tiếng Anh
                        </div>
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)">
                            Phân tích nhu cầu người dùng
                        </div>
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)">
                            Sử dụng Pivotal Tracker
                        </div>
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)">
                            Vẽ Wireframe
                        </div>
                    </div>
                </div>

                <div id="cv-item-skill" class="cv-item movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="color: white">
                            <b>CHỨNG CHỈ</b>
                        </div>
                    </div>
                    <div class="cv-item-body movable copyable">
                        <div style="display: flex; justify-content: space-between;">
                            <span class="cv-item-content padding" contenteditable="true"
                                onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                <b>GOOGLE ADWORDS</b>
                            </span>
                            <span class="cv-item-content padding" contenteditable="true"
                                onclick="onClickElement(this)"
                                style="line-height: 24px; font-size: 14px;">11/2016
                            </span>
                        </div>

                        <div class="cv-item-content padding" contenteditable="true"
                            onclick="onClickElement(this)"
                            style="margin-top: -20px; font-size: 14px; line-height: 25px;">
                            Đọc và thi 2 chứng chỉ trong 14 ngày
                            <ul>
                                <li>AdWords căn bản</li>
                                <li>Quảng cáo tìm kiếm</li>
                            </ul>
                        </div>
                    </div>

                    <div class="cv-item-body movable copyable">
                        <div style="display: flex; justify-content: space-between;">
                            <span class="cv-item-content padding" contenteditable="true"
                                onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                <b>TOEIC</b>
                            </span>
                            <span class="cv-item-content padding" contenteditable="true"
                                onclick="onClickElement(this)"
                                style="line-height: 24px; font-size: 14px;">12/2012
                            </span>
                        </div>

                        <div class="cv-item-content padding" contenteditable="true"
                            onclick="onClickElement(this)"
                            style="margin-top: -20px; font-size: 14px; line-height: 25px;">
                            750 điểm. Có thể:
                            <ul>
                                <li>Đọc và viết tài liệu tham khảo</li>
                                <li>Viết business và support email</li>
                                <li>Nghe, nói và take note khi thảo luận công việc qua các buổi họp, call với
                                    khách hàng</li>
                            </ul>
                        </div>
                    </div>
                </div>


            </div>

            <div class="cv-col cv-col-8 padding" onmouseenter="onMouseEnterColumn(this)"
                onmouseleave="onMouseLeaveColumn(this)">
                <div id="cv-item-name-card" class="cv-item movable padding" style="margin-top: -10px;">
                    <div class="cv-item-body">
                        <div class="cv-item-fullname" contenteditable="true" onclick="onClickElement(this)"
                            style="font-size: 24px; text-transform: uppercase;">
                            <b>ĐINH PHƯƠNG THẢO</b>
                        </div>
                        <div class="cv-item-nominee" contenteditable="true" onclick="onClickElement(this)"
                            style="font-size: 16px; letter-spacing: 2px;">
                            <b>Product Manager</b>
                        </div>
                    </div>
                </div>

                <div class="cv-item cv-item-career-goals movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)">
                            <b>MỤC TIÊU NGHỀ NGHIỆP</b>
                        </div>
                    </div>
                    <div class="cv-item-body">
                        <div class="cv-item-content padding" contenteditable="true"
                            onclick="onClickElement(this)"
                            style="line-height: 24px; text-align: justify; font-size: 14px;">
                            Với hơn hai năm kinh nghiệm ở các vị trí Product Manager, Business Analyst, trong
                            việc hỗ trợ nhóm Agile, tạo, sắp xếp mức độ ưu tiên và quản lý backlog; các chứng
                            chỉ TOEIC 750, Google Adwards và bằng Thạc sỹ Quản trị kinh doanh; tôi mong muốn tận
                            dụng các kỹ năng và kiến thức của mình để đóng góp cho công ty với vai trò là
                            Product Manager.
                        </div>
                    </div>
                </div>

                <div id="cv-item-work-experience" class="cv-item movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)">
                            <b>KINH NGHIỆM LÀM VIỆC</b>
                        </div>
                    </div>
                    <div class="cv-item-body movable copyable">
                        <div style="display: flex; justify-content: space-between;">
                            <span class="cv-item-content padding" contenteditable="true"
                                onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                <b>PRODUCT MANAGER</b>
                            </span>
                            <span class="cv-item-content padding" contenteditable="true"
                                onclick="onClickElement(this)"
                                style="line-height: 24px; font-size: 14px;"><b>03/2017 - 03/2018</b></span>
                        </div>
                        <div class="cv-item-content padding" contenteditable="true"
                            onclick="onClickElement(this)" style="margin-top: -20px; font-size: 14px;">
                            <b><i>Digital Innovation</i></b>
                        </div>
                        <div class="cv-item-content padding" contenteditable="true"
                            onclick="onClickElement(this)"
                            style="margin-top: -20px; font-size: 14px; text-align: justify; line-height: 25px;">
                            Cung cấp thông tin, định hướng và hỗ trợ nhóm Agile trong quá trình phát triển phần
                            mềm:
                            <ul>
                                <li>Làm việc với người dùng/ khách hàng, các bên liên quan và nhóm delivery để
                                    thu thập thông tin.</li>
                                <li>Thảo luận với developer, tester và BA để làm rõ và đảm bảo chức năng phù hợp
                                    với mong đợi của người dùng.</li>
                                <li>Chịu trách nhiệm tạo, lên danh sách và sắp xếp thứ tự ưu tiên của backlog
                                    cho sản phẩm web.</li>
                                <li>Làm việc với Project Manager để lên kế hoạch, chương trình dự phòng, đảm bảo
                                    sản phẩm đúng với tầm nhìn và lộ trình.</li>
                            </ul>
                        </div>
                    </div>

                    <div class="cv-item-body movable copyable">
                        <div style="display: flex; justify-content: space-between;">
                            <span class="cv-item-content padding" contenteditable="true"
                                onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                <b>BUSINESS ANALYST</b>
                            </span>
                            <span class="cv-item-content padding" contenteditable="true"
                                onclick="onClickElement(this)"
                                style="line-height: 24px; font-size: 14px;"><b>02/2016 - 03/2017</b></span>
                        </div>
                        <div class="cv-item-content padding" contenteditable="true"
                            onclick="onClickElement(this)" style="margin-top: -20px; font-size: 14px;">
                            <b><i>Insign Group</i></b>
                        </div>
                        <div class="cv-item-content padding" contenteditable="true"
                            onclick="onClickElement(this)"
                            style="margin-top: -20px; font-size: 14px; text-align: justify; line-height: 25px;">
                            Dựa trên các thông tin từ người dùng, khách hàng và Product owner, tiến hành phân
                            tích và làm việc cùng nhóm Agile để phát triển sản phẩm web:
                            <ul>
                                <li>Làm việc trực tiếp với người dùng cuối để tìm hiểu và phân tích những khó
                                    khăn khi sử dụng sản phẩm.</li>
                                <li>Phối hợp với developer và tester để cải thiện UI/UX và logic cho các chức
                                    năng của sản phẩm.</li>
                                <li>Chịu trách nhiệm về phát triển cải tiến liên tục, tạo và sắp xếp các story
                                    sau khi thảo luận.</li>
                                <li>Sắp xếp mức độ ưu tiên làm việc cho nhóm Agile và xem xét các backlog còn
                                    lại.</li>
                                <li>Báo cáo KPI Delivery với Project Manager và CTO.</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `,
    'hien-dai': `
        <div class="cv-row" style="background-color: rgb(255, 210, 63);">
            <div class="cv-col cv-col-3 padding" onmouseenter="onMouseEnterColumn(this)"
                onmouseleave="onMouseLeaveColumn(this)">

                <div class="cv-item cv-item-avatar movable padding" onclick="onClickElement(this)">
                    <div class="cv-item-body">
                        <img src="/cv-builder/images/girl1.jpeg" alt="">
                    </div>
                </div>
            </div>

            <div class="cv-col cv-col-9 padding" onmouseenter="onMouseEnterColumn(this)"
                onmouseleave="onMouseLeaveColumn(this)"
                style="font-family: system-ui; font-size: 16px; line-height: 24px;">

                <div class="cv-item cv-item-name-card movable padding"
                    style="font-family: system-ui;font-size: 16px;line-height: 24px;text-align: center;">
                    <div class="cv-item-body"
                        style="font-family: system-ui; font-size: 16px; line-height: 24px;">
                        <div class="cv-item-fullname" contenteditable="true" onclick="onClickElement(this)"
                            style="font-size: 48px; font-family: system-ui; line-height: 72px;">
                            <b>NGUYỄN THÙY LINH</b>
                        </div>
                        <div class="cv-item-nominee" contenteditable="true" onclick="onClickElement(this)"
                            style="font-family: system-ui; font-size: 24px; line-height: 36px;">
                            <b>Nhân viên kinh doanh</b>
                        </div>
                    </div>
                </div>


                <div class="d-none" id="cv-btn-add-item" onclick="addItem(this)" style="top: 226.125px;">
                    <div class="btn">
                        <i class="fa-solid fa-plus"></i>
                    </div>
                    <!-- <div>Thêm mục</div> -->
                </div>
            </div>
        </div>

        <div class="cv-row padding" style="font-family: system-ui; font-size: 16px; line-height: 24px;">
            <div class="cv-col cv-col-12" onmouseenter="onMouseEnterColumn(this)"
                onmouseleave="onMouseLeaveColumn(this)" style="min-height: unset;">

                <div class="cv-item cv-item-contact-info movable"
                    style="font-family: system-ui; font-size: 16px; line-height: 24px;">
                    <div class="cv-item-header">

                    </div>
                    <div class="cv-item-body padding"
                        style="display: flex; justify-content: space-between; font-family: system-ui; font-size: 16px; line-height: 24px;">
                        <div class="cv-item-email">
                            <i class="fa fa-envelope"></i>
                            <span contenteditable="true" onclick="onClickElement(this)" class=""
                                style="font-family: system-ui; font-size: 16px; line-height: 24px;">thao.dinh@dion.vn</span>
                        </div>

                        <div class="cv-item-phone">
                            <i class="fa fa-circle-phone"></i>
                            <span contenteditable="true" onclick="onClickElement(this)" class=""
                                style="font-family: system-ui; font-size: 16px; line-height: 24px;">0906 799
                                9xx</span>
                        </div>

                        <div class="cv-item-phone">
                            <i class="fa fa-globe"></i>
                            <span contenteditable="true" onclick="onClickElement(this)" class=""
                                style="font-family: system-ui; font-size: 16px; line-height: 24px;">facebook.com/thao.dinh</span>
                        </div>

                        <div class="cv-item-phone">
                            <i class="fa fa-location-dot"></i>
                            <span contenteditable="true" onclick="onClickElement(this)" class=""
                                style="font-family: system-ui; font-size: 16px; line-height: 24px;">Hoàng Mai -
                                Hà
                                Nội</span>
                        </div>
                    </div>
                </div>



                <div class="d-none" id="cv-btn-add-item" onclick="addItem(this)" style="top: 100px;">
                    <div class="btn">
                        <i class="fa-solid fa-plus"></i>
                    </div>
                    <!-- <div>Thêm mục</div> -->
                </div>
                <div class="d-none" id="cv-btn-add-item" onclick="addItem(this)" style="top: 44px;">
                    <div class="btn">
                        <i class="fa-solid fa-plus"></i>
                    </div>
                    <!-- <div>Thêm mục</div> -->
                </div>
            </div>
        </div>

        <div class="cv-row padding">
            <div class="cv-col cv-col-12" onmouseenter="onMouseEnterColumn(this)"
                onmouseleave="onMouseLeaveColumn(this)">

                <div class="cv-item cv-item-career-goals movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="font-family: system-ui; font-size: 18px; line-height: 24px;">
                            <b>MỤC TIÊU NGHỀ NGHIỆP</b>
                        </div>
                    </div>
                    <div class="cv-item-body">
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)"
                            style="line-height: 22.4px; text-align: justify; font-size: 14px; font-family: system-ui;">
                            Với hơn hai năm kinh nghiệm ở các vị trí Product Manager, Business Analyst, trong
                            việc hỗ trợ nhóm Agile, tạo, sắp xếp mức độ ưu tiên và quản lý backlog; các chứng
                            chỉ TOEIC 750, Google Adwards và bằng Thạc sỹ Quản trị kinh doanh; tôi mong muốn tận
                            dụng các kỹ năng và kiến thức của mình để đóng góp cho công ty với vai trò là
                            Product Manager.
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="cv-row padding">
            <div class="cv-col cv-col-6" onmouseenter="onMouseEnterColumn(this)"
                onmouseleave="onMouseLeaveColumn(this)">

                <div class="cv-item cv-item-career-goals movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="font-family: system-ui; font-size: 18px; line-height: 24px;">
                            <b>HỌC VẤN</b>
                        </div>
                    </div>
                    <div class="cv-item-body">
                        <div style="color: #ffd23f" class="cv-item-branch" contenteditable="true"
                            onclick="onClickElement(this)">
                            <b>THẠC SỸ QUẢN TRỊ KINH DOANH</b>
                        </div>
                        <div class="cv-item-university">
                            <div style="display: flex; justify-content: space-between;">
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                    <b>ĐẠI HỌC KINH TẾ</b>
                                </span>
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)"
                                    style="line-height: 24px; font-size: 14px;"><b>10/2010 - 05/2014</b></span>
                            </div>
                        </div>
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)"
                            style="text-align: justify;">
                            Luận án: "Sự tác động của thương hiệu điện thoại và thương hiệu nhà bán lẻ đến sự
                            quay lại của người tiêu dùng"
                            <ul>
                                <li>Sử dụng kỹ thuật phỏng vấn chuyên gia, phỏng vấn nhóm và phát phiếu khảo sát
                                    để thu thập dữ liệu.</li>
                                <li>Sử dụng SEM, SPSS và Excel để thống kê và phân tích dữ liệu.</li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="cv-item cv-item-career-goals movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="font-family: system-ui; font-size: 18px; line-height: 24px;">
                            <b>HOẠT ĐỘNG</b>
                        </div>
                    </div>
                    <div class="cv-item-body">
                        <div class="cv-item-branch" style="color: #ffd23f" contenteditable="true"
                            onclick="onClickElement(this)">
                            <b>TÌNH NGUYỆN VIÊN</b>
                        </div>
                        <div class="cv-item-university">
                            <div style="display: flex; justify-content: space-between;">
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                    <b>Nhóm tình nguyện JOBI</b>
                                </span>
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)"
                                    style="line-height: 24px; font-size: 14px;"><b>10/2013 - 08/2014</b></span>
                            </div>
                        </div>
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)"
                            style="text-align: justify;">
                            Luận án: "Sự tác động của thương hiệu điện thoại và thương hiệu nhà bán lẻ đến sự
                            quay lại của người tiêu dùng"
                            <ul>
                                <li>Sử dụng kỹ thuật phỏng vấn chuyên gia, phỏng vấn nhóm và phát phiếu khảo sát
                                    để thu thập dữ liệu.</li>
                                <li>Sử dụng SEM, SPSS và Excel để thống kê và phân tích dữ liệu.</li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="cv-item cv-item-career-goals movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="font-family: system-ui; font-size: 18px; line-height: 24px;">
                            <b>GIẢI THƯỞNG</b>
                        </div>
                    </div>
                    <div class="cv-item-body">
                        <div class="cv-item-university">
                            <div style="display: flex; justify-content: space-between;">
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                    <b>Nhân viên xuất sắc của năm công ty JOBI</b>
                                </span>
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)"
                                    style="line-height: 24px; font-size: 14px;"><b>2014</b></span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="cv-item cv-item-career-goals movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="font-family: system-ui; font-size: 18px; line-height: 24px;">
                            <b>NGƯỜI THAM CHIẾU</b>
                        </div>
                    </div>
                    <div class="cv-item-body">
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)">
                            Anh Nguyễn Văn A - Trưởng phòng Marketing
                            <br>
                            Công ty JOBI
                            <br>
                            Điện thoại: 0987654321
                        </div>
                    </div>
                </div>
            </div>
            <div class="cv-col cv-col-6" onmouseenter="onMouseEnterColumn(this)"
                onmouseleave="onMouseLeaveColumn(this)">
                <div class="cv-item cv-item-career-goals movable padding">
                    <div class="cv-item-header">
                        <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                            style="font-family: system-ui; font-size: 18px; line-height: 24px;">
                            <b>KINH NGHIỆM LÀM VIỆC</b>
                        </div>
                    </div>
                    <div class="cv-item-body">
                        <div style="color: #ffd23f" class="cv-item-branch" contenteditable="true"
                            onclick="onClickElement(this)">
                            <b>PRODUCT MANAGER</b>
                        </div>
                        <div class="cv-item-university">
                            <div style="display: flex; justify-content: space-between;">
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                    <b>Digital Innovation</b>
                                </span>
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)"
                                    style="line-height: 24px; font-size: 14px;"><b>03/2017 - 03/2018</b></span>
                            </div>
                        </div>
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)"
                            style="text-align: justify;">
                            Cung cấp thông tin, định hướng và hỗ trợ nhóm Agile trong quá trình phát triển phần
                            mềm:
                            <ul>
                                <li>Làm việc với người dùng/ khách hàng, các bên liên quan và nhóm delivery để
                                    thu thập thông tin.</li>
                                <li>Thảo luận với developer, tester và BA để làm rõ và đảm bảo chức năng phù hợp
                                    với mong đợi của người dùng.</li>
                                <li>Chịu trách nhiệm tạo, lên danh sách và sắp xếp thứ tự ưu tiên của backlog
                                    cho sản phẩm web.</li>
                                <li>Làm việc với Project Manager để lên kế hoạch, chương trình dự phòng, đảm bảo
                                    sản phẩm đúng với tầm nhìn và lộ trình.</li>
                            </ul>
                        </div>
                    </div>

                    <div class="cv-item-body">
                        <div style="color: #ffd23f" class="cv-item-branch" contenteditable="true"
                            onclick="onClickElement(this)">
                            <b>BUSINESS ANALYST</b>
                        </div>
                        <div class="cv-item-university">
                            <div style="display: flex; justify-content: space-between;">
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                                    <b>Insign Group</b>
                                </span>
                                <span class="cv-item-content" contenteditable="true"
                                    onclick="onClickElement(this)"
                                    style="line-height: 24px; font-size: 14px;"><b>02/2016 - 03/2017</b></span>
                            </div>
                        </div>
                        <div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)"
                            style="text-align: justify;">
                            Dựa trên các thông tin từ người dùng, khách hàng và Product owner, tiến hành phân
                            tích và làm việc cùng nhóm Agile để phát triển sản phẩm web:
                            <ul>
                                <li>Làm việc trực tiếp với người dùng cuối để tìm hiểu và phân tích những khó
                                    khăn khi sử dụng sản phẩm.</li>
                                <li>Phối hợp với developer và tester để cải thiện UI/UX và logic cho các chức
                                    năng của sản phẩm.</li>
                                <li>Chịu trách nhiệm về phát triển cải tiến liên tục, tạo và sắp xếp các story
                                    sau khi thảo luận.</li>
                                <li>Sắp xếp mức độ ưu tiên làm việc cho nhóm Agile và xem xét các backlog còn
                                    lại.</li>
                                <li>Báo cáo KPI Delivery với Project Manager và CTO.</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
}

fontFamilies = {
    'Alegreya': 'Alegreya',
    'Alfa Slab One': 'Alfa Slab One',
    'Amatic SC': 'Amatic SC',
    'Anton': 'Anton',
    'Archivo Narrow': 'Archivo Narrow',
    'Arima': 'Arima',
    'Arimo': 'Arimo',
    'Asap': 'Asap',
    'Baloo 2': 'Baloo 2',
    'Bangers': 'Bangers',
    'Bevan': 'Bevan',
    'Bungee': 'Bungee',
    'Cabin': 'Cabin',
    'Chonburi': 'Chonburi',
    'Coiny': 'Coiny',
    'Comfortaa': 'Comfortaa',
    'Cormorant': 'Cormorant',
    'Cousine': 'Cousine',
    'Cuprum': 'Cuprum',
    'Dancing Script': 'Dancing Script',
    'David Libre': 'David Libre',
    'EB Garamond': 'EB Garamond',
    'Exo': 'Exo',
    'Fira Sans': 'Fira Sans',
    'Francois One': 'Francois One',
    'Gentium Plus': 'Gentium Plus',
    'Inconsolata': 'Inconsolata',
    'Josefin Sans': 'Josefin Sans',
    'Judson': 'Judson',
    'Jura': 'Jura',
    'Kanit': 'Kanit',
    'Lalezar': 'Lalezar',
    'Lemonada': 'Lemonada',
    'Lobster': 'Lobster',
    'Lora': 'Lora',
    'Maitree': 'Maitree',
    'Maven Pro': 'Maven Pro',
    'Merriweather': 'Merriweather',
    'Metrophobic': 'Metrophobic',
    'Mitr': 'Mitr',
    'Montserrat Alternates': 'Montserrat Alternates',
    'Mulish': 'Mulish',
    'Noticia Text': 'Noticia Text',
    'Noto Sans': 'Noto Sans',
    'Nunito': 'Nunito',
    'Old Standard TT': 'Old Standard TT',
    'Open Sans': 'Open Sans',
    'Oswald': 'Oswald',
    'Pacifico': 'Pacifico',
    'Patrick Hand': 'Patrick Hand',
    'Pattaya': 'Pattaya',
    'Paytone One': 'Paytone One',
    'Philosopher': 'Philosopher',
    'Play': 'Play',
    'Playfair Display': 'Playfair Display',
    'Podkova': 'Podkova',
    'Pridi': 'Pridi',
    'Prompt': 'Prompt',
    'Quicksand': 'Quicksand',
    'Roboto': 'Roboto',
    'Rokkitt': 'Rokkitt',
    'Sigmar One': 'Sigmar One',
    'Source Code Pro': 'Source Code Pro',
    'Space Mono': 'Space Mono',
    'Taviraj': 'Taviraj',
    'Tinos': 'Tinos',
    'Trirong': 'Trirong',
    'Varela Round': 'Varela Round',
    'Vollkorn': 'Vollkorn',
    'VT323': 'VT323',
    'Yanone Kaffeesatz': 'Yanone Kaffeesatz',
    'Yeseva One': 'Yeseva One',
    "Arial": "Arial",
    "Verdana": "Verdana",
    "Tahoma": "Tahoma",
    "Trebuchet MS": "Trebuchet MS",
    "Times New Roman": "Times New Roman",
    "Georgia": "Georgia",
    "Garamond": "Garamond",
    "Courier New": "Courier New",
    "system-ui": "System UI"
}

fontSizes = {
    "10px": "10px",
    "12px": "12px",
    "13px": "13px",
    "14px": "14px",
    "16px": "16px",
    "18px": "18px",
    "20px": "20px",
    "22px": "22px",
    "23px": "23px",
    "24px": "24px",
    "26px": "26px",
    "30px": "30px",
    "32px": "32px",
    "36px": "36px",
    "48px": "48px",
};

lineHeights = {
    "1.2": "1.2",
    "1.3": "1.3",
    "1.4": "1.4",
    "1.5": "1.5",
    "1.6": "1.6",
    "1.7": "1.7",
    "1.8": "1.8",
    "1.9": "1.9",
    "2": "2.0",
}

fontFamilies2 = {
    "Arial": "Arial",
    "Verdana": "Verdana",
    "Tahoma": "Tahoma",
    "Trebuchet MS": "Trebuchet MS",
    "Times New Roman": "Times New Roman",
    "Georgia": "Georgia",
    "Garamond": "Garamond",
    "Courier New": "Courier New",
    "system-ui": "System UI"
}

itemHtml = {
    "avatar": `
        <div class="cv-item cv-item-avatar movable padding">
            <div class="cv-item-body">
                <img onclick="onClickElement(this)" src="images/girl1.jpeg" alt="Ảnh đại diện">
            </div>
        </div>
    `,
    "name-card": `
        <div class="cv-item cv-item-name-card movable padding"
            style="font-family: system-ui;font-size: 16px;line-height: 24px;text-align: center;">
            <div class="cv-item-body"
                style="font-family: system-ui; font-size: 16px; line-height: 24px;">
                <div class="cv-item-fullname" contenteditable="true" onclick="onClickElement(this)"
                    style="font-size: 48px; font-family: system-ui; line-height: 72px;">
                    <b>NGUYỄN THÙY LINH</b>
                </div>
                <div class="cv-item-nominee" contenteditable="true" onclick="onClickElement(this)"
                    style="font-family: system-ui; font-size: 24px; line-height: 36px;">
                    <b>Nhân viên kinh doanh</b>
                </div>
            </div>
        </div>
    `,
    "contact-info": `
        <div class="cv-item cv-item-contact-info movable padding">
            <div class="cv-item-header">
                <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)"
                    style="color: white">
                    <b>THÔNG TIN CÁ NHÂN</b>
                </div>
            </div>
            <div class="cv-item-body padding">
                <div class="cv-item-email">
                    <i class="fa fa-envelope"></i>
                    <span contenteditable="true" onclick="onClickElement(this)">thao.dinh@dion.vn</span>
                </div>

                <div class="cv-item-phone">
                    <i class="fa fa-circle-phone"></i>
                    <span contenteditable="true" onclick="onClickElement(this)">0906 799 9xx</span>
                </div>

                <div class="cv-item-phone">
                    <i class="fa fa-globe"></i>
                    <span contenteditable="true"
                        onclick="onClickElement(this)">facebook.com/thao.dinh</span>
                </div>

                <div class="cv-item-phone">
                    <i class="fa fa-location-dot"></i>
                    <span contenteditable="true" onclick="onClickElement(this)">Hoàng Mai - Hà
                        Nội</span>
                </div>
            </div>
        </div>
    `,
    "career-goals": `
        <div class="cv-item cv-item-career-goals movable padding">
            <div class="cv-item-header">
                <div class="cv-item-title" contenteditable="true" onclick="onClickElement(this)">
                    <b>MỤC TIÊU NGHỀ NGHIỆP</b>
                </div>
            </div>
            <div class="cv-item-body">
                <div class="cv-item-content padding" contenteditable="true"
                    onclick="onClickElement(this)"
                    style="line-height: 24px; text-align: justify; font-size: 14px;">
                    Với hơn hai năm kinh nghiệm ở các vị trí Product Manager, Business Analyst, trong
                    việc hỗ trợ nhóm Agile, tạo, sắp xếp mức độ ưu tiên và quản lý backlog; các chứng
                    chỉ TOEIC 750, Google Adwards và bằng Thạc sỹ Quản trị kinh doanh; tôi mong muốn tận
                    dụng các kỹ năng và kiến thức của mình để đóng góp cho công ty với vai trò là
                    Product Manager.
                </div>
            </div>
        </div>
    `
}

