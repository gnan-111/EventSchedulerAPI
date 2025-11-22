import { useState } from "react";
import { Menu, X, CalendarDays } from "lucide-react";
import "./Navbar.css";

const Navbar = () => {
  const [mobileOpen, setMobileOpen] = useState(false);

  return (
    <nav className="nav-container">
      <div className="nav-inner">

        {/* Left Section */}
        <div className="nav-left">
          <CalendarDays size={26} className="nav-logo" />
          <span className="nav-title">Event Scheduler</span>
        </div>

        {/* Desktop Links */}
        <div className="nav-links">
          <a href="#" className="nav-link active">Home</a>
          <a href="#" className="nav-link">Events</a>
          <a href="#" className="nav-link">Teams</a>
          <a href="#" className="nav-link">About</a>
        </div>

        {/* Mobile Menu Icon */}
        <button 
          className="nav-mobile-btn" 
          onClick={() => setMobileOpen(prev => !prev)}
        >
          {mobileOpen ? <X size={24} /> : <Menu size={24} />}
        </button>
      </div>

      {/* Mobile Menu */}
      {mobileOpen && (
        <div className="nav-mobile-menu">
          <a href="#" className="nav-mobile-link active">Home</a>
          <a href="#" className="nav-mobile-link">Events</a>
          <a href="#" className="nav-mobile-link">Teams</a>
          <a href="#" className="nav-mobile-link">About</a>
        </div>
      )}
    </nav>
  );
};

export default Navbar;
