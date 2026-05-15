const searchInput = document.getElementById("searchInput");
const roleFilter = document.getElementById("roleFilter");
const themeToggle = document.getElementById("themeToggle");
const resetFormBtn = document.getElementById("resetFormBtn");

function filterCharacters() {
    const cards = document.querySelectorAll(".character-item");
    const searchValue = searchInput.value.toLowerCase().trim();
    const selectedRole = roleFilter.value;

    cards.forEach((card) => {
        const matchSearch = card.dataset.name.includes(searchValue);
        const matchRole = selectedRole === "all" || card.dataset.role === selectedRole;
        card.style.display = matchSearch && matchRole ? "" : "none";
    });
}

if (searchInput) {
    searchInput.addEventListener("input", filterCharacters);
}

if (roleFilter) {
    roleFilter.addEventListener("change", filterCharacters);
}

if (themeToggle) {
    themeToggle.addEventListener("click", function () {
        const html = document.documentElement;
        const currentTheme = html.getAttribute("data-theme");
        const newTheme = currentTheme === "dark" ? "light" : "dark";

        html.setAttribute("data-theme", newTheme);
        themeToggle.textContent = newTheme === "dark" ? "🌙" : "☀️";
    });
}

if (resetFormBtn) {
    resetFormBtn.addEventListener("click", function () {
        document.getElementById("characterId").value = "";
        document.getElementById("characterName").value = "";
        document.getElementById("characterRole").value = "";
        document.getElementById("characterDescription").value = "";
        document.getElementById("characterImage").value = "";
    });
}

document.querySelectorAll(".edit-character-btn").forEach((btn) => {
    btn.addEventListener("click", function () {
        document.getElementById("characterId").value = this.dataset.id;
        document.getElementById("characterName").value = this.dataset.name;
        document.getElementById("characterRole").value = this.dataset.role;
        document.getElementById("characterDescription").value = this.dataset.description;
        document.getElementById("characterImage").value = this.dataset.image;
        window.location.hash = "admin";
    });
});